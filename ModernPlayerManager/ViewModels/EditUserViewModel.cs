using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class EditUserViewModel : NotificationBase
    {
        public string Email { get; set; }
        public string Username { get; set; }

        private UserProfile userProfile;

        public UserProfile UserProfile
        {
            get => userProfile;
            set
            {
                userProfile = value;
                OnPropertyChanged();
            }
        }

        public IUserApi UserApi = RestService.For<IUserApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});


        public AsyncCommand UpdateCommand { get; set; }

        public EditUserViewModel(UserProfile userProfile) {
            this.userProfile = userProfile;
            Email = userProfile.Email;
            Username = userProfile.Username;
            UpdateCommand = new AsyncCommand(this.Update);
        }

        public async Task Update() {
            UserProfile.Email = Email;
            UserProfile.Username = Username;

            var dto = new UpdateUserProfileDTO()
            {
                Email = Email,
                Username = Username,
                Image = null,
                Password = null
            };

            var id = (await UserApi.SearchUser(userProfile.Username))[0]?.Id;
            await UserApi.UpdateUserProfile(dto, id);
        }
    }
}