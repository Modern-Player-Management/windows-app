using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ModernPlayerManager.Pages;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class RegisterViewModel : NotificationBase
    {
        private bool _loading = false;
        private string _username;
        private string _email;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                ClickRegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                ClickRegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                ClickRegisterCommand.RaiseCanExecuteChanged();
            }
        }

        public ClickRegisterCommand ClickRegisterCommand { get; set; }

        public RegisterViewModel() {
            ClickRegisterCommand = new ClickRegisterCommand(this);
        }

        public bool Loading
        {
            get => _loading;
            set
            {
                _loading = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(NotLoading));
            }
        }

        public bool NotLoading => !_loading;

        public async void Register() {
            Loading = true;

            var api = RestService.For<ILoginApi>(new HttpClient() { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });

            var dto = new RegisterDTO() { Username = this.Username, Password = this.Password, Email = this.Email};

            try
            {
                await api.Register(dto);
                (Window.Current.Content as Frame)?.Navigate(typeof(LoginPage));
            }
            catch (Exception e)
            {
                Loading = false;
                var dialog = new ContentDialog
                {
                    Title = "Login Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                var result = await dialog.ShowAsync();
            }
        }
    }
}