using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.ViewModels.DataViewModels;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class TeamPageViewModel : NotificationBase
    {
        private readonly string _teamId;

        public ITeamApi TeamApi = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});


        public IFileApi FileApi = RestService.For<IFileApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });

        private TeamViewModel _team;
        private bool _loading = true;

        private BitmapImage _image;

        public BitmapImage TeamImage
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        public TeamViewModel Team
        {
            get => _team;
            set
            {
                _team = value;
                OnPropertyChanged();
            }
        }

        public TeamPageViewModel(string teamId) {
            _teamId = teamId;
        }

        public bool Loading {
            get => _loading;
            set {
                _loading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NotLoading));
            }
        }


        public bool NotLoading => !_loading;

        public async Task FetchTeam() {
            try {
                Team = new TeamViewModel(await TeamApi.GetTeam(_teamId));
                Loading = false;

                Guid x;
                if(Team.Image != null && Guid.TryParse(Team.Image, out x)) {
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

        public async Task<BitmapImage> GetBitmapAsync(byte[] data)
        {
            var bitmapImage = new BitmapImage();

            using (var stream = new InMemoryRandomAccessStream())
            {
                using (var writer = new DataWriter(stream))
                {
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
    }
}