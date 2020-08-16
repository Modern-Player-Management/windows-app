using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Notifications;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Microsoft.Toolkit.Uwp.UI.Controls;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class TeamViewModel : NotificationBase
    {
        public MainViewModel MainViewModel { get; set; } = ViewModelsLocator.Instance.MainViewModel;

        private readonly string teamId;

        public ITeamApi TeamApi = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});


        public IFileApi FileApi = RestService.For<IFileApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        public AsyncCommand DeleteTeamCommand { get; private set; }
        public RelayCommand OpenAddPlayerToTeamDialog { get; private set; }


        private Team team;
        private bool loading = true;

        private BitmapImage image;

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
            }
        }

        private ObservableCollection<EventListItemViewModel> teamEvents;

        public ObservableCollection<EventListItemViewModel> TeamEvents
        {
            get => teamEvents;
            set
            {
                teamEvents = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<GameListItemViewModel> teamGames;

        public ObservableCollection<GameListItemViewModel> TeamGames
        {
            get => teamGames;
            set
            {
                teamGames = value;
                OnPropertyChanged();
            }
        }

        public TeamViewModel(string teamId) {
            this.teamId = teamId;
            this.OpenAddPlayerToTeamDialog = new RelayCommand(AddPlayerToTeam, IsUserTeamManager);
            this.DeleteTeamCommand = new AsyncCommand(DeleteTeam, IsUserTeamManager);
        }

        private bool IsUserTeamManager() => Team?.IsCurrentUserManager ?? false;

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


        public bool NotLoading => !loading;


        public async Task FetchTeam() {
            try {
                Team = await TeamApi.GetTeam(teamId);
                TeamEvents = new ObservableCollection<EventListItemViewModel>(Team.Events.Select(evt => new EventListItemViewModel(evt)));
                TeamGames = new ObservableCollection<GameListItemViewModel>(Team.Games.Select(game => new GameListItemViewModel(game, Team.IsCurrentUserManager)));
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
            if(buttonClicked == ContentDialogResult.Primary) {
                Team.Players.Add(dialog.ViewModel.SelectedUser);
            }
        }


    }
}