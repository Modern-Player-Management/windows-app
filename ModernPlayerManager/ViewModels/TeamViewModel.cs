using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp.UI.Controls;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Models;
using ModernPlayerManager.Pages;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class TeamViewModel : NotificationBase
    {
        #region Fields

        private Team team;
        private bool loading = true;
        private BitmapImage image;
        private readonly string teamId;
        public bool NotLoading => !loading;

        #endregion

        #region Properties

        public MainViewModel MainViewModel { get; set; } = ViewModelsLocator.Instance.MainViewModel;


        public BitmapImage TeamImage
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public Team Team
        {
            get => team;
            set
            {
                team = value;
                OnPropertyChanged();
                OpenAddPlayerToTeamDialog.RaiseCanExecuteChanged();
                DeleteTeamCommand.RaiseCanExecuteChanged();
                OpenEditTeamDialogCommand.RaiseCanExecuteChanged();
            }
        }


        public bool Loading
        {
            get => loading;
            set
            {
                loading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NotLoading));
            }
        }

        #endregion

        public TeamViewModel(string teamId) {
            this.teamId = teamId;
            this.OpenAddPlayerToTeamDialog = new RelayCommand(AddPlayerToTeam, IsUserTeamManager);
            this.DeleteTeamCommand = new AsyncCommand(DeleteTeam, IsUserTeamManager);
            this.OpenEditTeamDialogCommand = new RelayCommand(UpdateTeamDialogCommand, IsUserTeamManager);
            this.NavigateToStatsCommand = new RelayCommand(NavigateToStats);
            this.DeleteDiscrepancyCommand =
                new AsyncCommandParams<Discrepancy>(DeleteDiscrepancy, DiscrepancyIsFromCurrentUser);
            this.AddDiscrepancyCommand = new RelayCommandParams<Event>(AddDiscrepancy);
            this.EditDiscrepancyCommand = new AsyncCommandParams<Discrepancy>(EditDiscrepancy);
            this.AddEventCommand = new AsyncCommand(AddEvent);
            this.EditEventCommand = new AsyncCommandParams<Event>(EditEvent);
            this.DeleteEventCommand = new AsyncCommandParams<Event>(DeleteEvent);
            this.UpdateEventPresenceCommand = new AsyncCommandParams<Event>(EditPresence);
            this.RemovePlayerCommand = new AsyncCommandParams<User>(RemovePlayer);
            this.UploadGameCommand = new AsyncCommand(AddGame);
            this.ShowGameDetailsCommand = new AsyncCommandParams<Game>(ShowGameDetails);
            this.DeleteGameCommand = new AsyncCommandParams<Game>(DeleteGame);
        }

        private bool IsUserTeamManager() => Team?.IsCurrentUserManager ?? false;

        private bool DiscrepancyIsFromCurrentUser(Discrepancy discrepancy) =>
            AuthenticatedHttpClientHandler.UserId == discrepancy.UserId;

        #region Commands

        public AsyncCommand DeleteTeamCommand { get; }
        public AsyncCommandParams<Discrepancy> DeleteDiscrepancyCommand { get;  }
        public AsyncCommandParams<Discrepancy> EditDiscrepancyCommand { get;  }
        public RelayCommand OpenAddPlayerToTeamDialog { get; }
        public RelayCommand OpenEditTeamDialogCommand { get;  }
        public RelayCommand NavigateToStatsCommand { get; }
        public RelayCommandParams<Event> AddDiscrepancyCommand { get; }
        public AsyncCommand AddEventCommand { get; }
        public AsyncCommandParams<Event> EditEventCommand { get; }
        public AsyncCommandParams<Event> DeleteEventCommand { get; }
        public AsyncCommandParams<Event> UpdateEventPresenceCommand { get; }
        public AsyncCommandParams<User> RemovePlayerCommand { get; }
        public AsyncCommand UploadGameCommand { get; }
        public AsyncCommandParams<Game> ShowGameDetailsCommand { get; }
        public AsyncCommandParams<Game> DeleteGameCommand { get; }

        #endregion

        #region Webservices

        public ITeamApi TeamApi = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});


        public IFileApi FileApi = RestService.For<IFileApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        public IDiscrepancyApi DiscrepancyApi = RestService.For<IDiscrepancyApi>(
            new HttpClient(new AuthenticatedHttpClientHandler())
                {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        public IEventApi EventApi = RestService.For<IEventApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        public IGameApi GameApi = RestService.For<IGameApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });



        #endregion

        #region Handlers

        public async Task ShowGameDetails(Game game)
        {
            var dialog = new GameDialog(game);
            await dialog.ShowAsync();
        }

        public async Task DeleteGame(Game game)
        {
            await GameApi.DeleteGame(game.Id);
            Team.Games.Remove(game);
        }

        public async Task AddGame()
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
            };
            picker.FileTypeFilter.Add(".replay");
            var file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                return;
            }

            var stream = await file.OpenStreamForReadAsync();
            using (var ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);

                try
                {
                    var newGame = await TeamApi.UploadGame(new ByteArrayPart(ms.ToArray(), file.Name), Team.Id);
                    Team.Games.Add(newGame);
                }
                catch (Exception e)
                {
                    var errorDialog = new ContentDialog
                    {
                        Title = "Error",
                        Content = e.Message,
                        CloseButtonText = "Ok"
                    };
                    await errorDialog.ShowAsync();
                }
            }
        }

        public async Task RemovePlayer(User player)
        {
            try
            {
                await TeamApi.RemovePlayer(Team.Id, player.Id);
                await FetchTeam();
            }
            catch (Exception e)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await errorDialog.ShowAsync();
            }
        }

        public async Task DeleteEvent(Event evt)
        {
            await EventApi.DeleteEvent(evt.Id);
            Team.Events.Remove(Team.Events.First(e => e.Id == evt.Id));
        }

        private async Task EditPresence(Event evt)
        {
            evt.Participations.First(p => p.UserId == AuthenticatedHttpClientHandler.UserId).Confirmed =
                evt.CurrentHasConfirmed;
            try
            {
                await EventApi.UpdatePresence(evt.Id,
                    new EventPresenceDTO()
                    {
                        Present = evt.Participations.First(p => p.UserId == AuthenticatedHttpClientHandler.UserId).Confirmed
                    });
            }
            catch (Exception e)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await errorDialog.ShowAsync();
            }
        }

        public async Task EditEvent(Event evt)
        {
            var dialog = new EventDialog(evt);
            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            try
            {
                var dto = dialog.ViewModel.EventDto;
                await EventApi.UpdateEvent(evt.Id, dialog.ViewModel.EventDto);
                evt.Name = dto.Name;
                evt.Description = dto.Description;
                evt.Start = dto.Start;
                evt.End = dto.End;
                evt.Type = dto.Type;

            }
            catch (Exception e)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await errorDialog.ShowAsync();
            }
        }

        public async Task AddEvent()
        {
            var dialog = new EventDialog();
            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                return;
            }

            try
            {
                var evt = await TeamApi.AddEventToTeam(this.Team.Id,dialog.ViewModel.EventDto);
                Team.Events.Add(evt);
            }
            catch (Exception e)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await errorDialog.ShowAsync();
            }
        }
        

        public async Task DeleteDiscrepancy(Discrepancy discrepancy) {
            try {
                await DiscrepancyApi.DeleteDiscrepancy(discrepancy.Id);
                var evt = Team.Events.First(e => e.Discrepancies.Contains(discrepancy));
                evt.Discrepancies.Remove(discrepancy);
            }
            catch (Exception e) {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }


        private async void AddDiscrepancy(Event evt) {
            var dialog = new DiscrepancyDialog(DialogMode.Create);
            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary || dialog.ViewModel.Dto == null) {
                return;
            }

            try {
                await EventApi.CreateDiscrepancy(evt.Id, dialog.ViewModel.Dto);
                await FetchTeam();
            }
            catch (Exception e) {
                Debug.Print(e.Message);
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await errorDialog.ShowAsync();
            }
            
        }

        private async Task EditDiscrepancy(Discrepancy discrepancy)
        {
            var dialog = new DiscrepancyDialog(DialogMode.Edit)
            {
                ViewModel =
                {
                    Type = discrepancy.Type.ToString(),
                    DelayLength = (uint) discrepancy.DelayLength,
                    Reason = discrepancy.Reason
                }
            };

            var result = await dialog.ShowAsync();

            if (result != ContentDialogResult.Primary || dialog.ViewModel.Dto == null)
            {
                return;
            }

            try
            {
                await DiscrepancyApi.UpdateDiscrepancy(discrepancy.Id, dialog.ViewModel.Dto);
                await FetchTeam();
            }
            catch (Exception e)
            {
                Debug.Print(e.Message);
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await errorDialog.ShowAsync();
            }
        }

        public async void UpdateTeamDialogCommand() {
            var dialog = new EditTeamDialog(Team);
            await dialog.ShowAsync();
        }

        public async Task FetchTeam() {
            try {
                Team = await TeamApi.GetTeam(teamId);
                Loading = false;

                Guid x;
                if (Team.Image != null && Guid.TryParse(Team.Image, out x)) {
                    var httpContent = await FileApi.GetFile(Team.Image);
                    var bytes = await httpContent.ReadAsByteArrayAsync();
                    TeamImage = await GetBitmapAsync(bytes);
                }
            }
            catch (Exception e) {
                Loading = false;
                var dialog = new ContentDialog
                {
                    Title = "Loading Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }

        public async Task DeleteTeam() {
            var deleteFileDialog = new ContentDialog
            {
                Title = "Delete this Team permanently?",
                Content = "If you delete this Team, you won't be able to recover it. Do you want to delete it?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            var result = await deleteFileDialog.ShowAsync();

            if (result != ContentDialogResult.Primary) {
                return;
            }

            try {
                await TeamApi.DeleteTeam(Team.Id);
                MainViewModel.Teams.Remove(MainViewModel.Teams.First(t => t.Id == Team.Id));
            }
            catch (Exception e) {
                var dialog = new ContentDialog
                {
                    Title = "Loading Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }

        public async Task<BitmapImage> GetBitmapAsync(byte[] data) {
            var bitmapImage = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream()) {
                using (var writer = new DataWriter(stream)) {
                    writer.WriteBytes(data);
                    await writer.StoreAsync();
                    await writer.FlushAsync();
                    writer.DetachStream();
                }

                stream.Seek(0);
                await bitmapImage.SetSourceAsync(stream);
            }

            return bitmapImage;
        }

        public async void AddPlayerToTeam() {
            var dialog = new AddPlayerToTeamDialog(Team);
            var buttonClicked = await dialog.ShowAsync();
            if (buttonClicked == ContentDialogResult.Primary) {
                Team.Players.Add(dialog.ViewModel.SelectedUser);
            }
        }

        public void NavigateToStats() {
            MainViewModel.ContentFrame.Navigate(typeof(StatsPage), Team.Id);
        }

        #endregion
    }
}