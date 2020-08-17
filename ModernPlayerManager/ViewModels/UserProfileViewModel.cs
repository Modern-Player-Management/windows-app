using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using ModernPlayerManager.Models;
using ModernPlayerManager.Pages;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class UserProfileViewModel : NotificationBase
    {
        private UserProfile userProfile;
        private BitmapImage image;

        public UserProfile UserProfile
        {
            get => userProfile;
            set
            {
                userProfile = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage UserProfileImage {
            get => image;
            set {
                image = value;
                OnPropertyChanged();
            }
        }

        public IFileApi FileApi = RestService.For<IFileApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });

        public IUserApi UserApi = RestService.For<IUserApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });

        public UserProfileViewModel() {
            FetchUserProfile();
        }

        public async void FetchUserProfile() {
            UserProfile = await UserApi.GetUserProfile();

            Guid x;
            if (UserProfile.Image != null && Guid.TryParse(UserProfile.Image, out x))
            {
                var httpContent = await FileApi.GetFile(UserProfile.Image);
                var bytes = await httpContent.ReadAsByteArrayAsync();
                UserProfileImage = await GetBitmapAsync(bytes);

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
