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
    public class LoginViewModel : NotificationBase
    {
        private bool _loading = false;

        private string _username;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                ClickLoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                ClickLoginCommand.RaiseCanExecuteChanged();
            }
        }

        public ClickLoginCommand ClickLoginCommand { get; set; }
        public ICommand NavigateToRegisterCommand { get; set; }

        public LoginViewModel() {
            ClickLoginCommand = new ClickLoginCommand(this);
            NavigateToRegisterCommand = new NavigateToRegisterCommand(this);
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

        public async void Login() {
            Loading = true;

            var api = RestService.For<ILoginApi>(new HttpClient(){BaseAddress = new Uri("https://api-mpm.herokuapp.com") });

            var dto = new LoginDTO() {Username = this.Username, Password = this.Password};

            try {
                var loggedUser = await api.Login(dto);
                AuthenticatedHttpClientHandler.Token = loggedUser.Token;
                (Window.Current.Content as Frame)?.Navigate(typeof(MainPage));
            } catch (Exception e) {
                Loading = false;
                ContentDialog dialog = new ContentDialog
                {
                    Title = "Login Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                ContentDialogResult result = await dialog.ShowAsync();
            }
        }

        public void NavigateToRegister() {
            (Window.Current.Content as Frame)?.Navigate(typeof(RegisterPage));
        }
    }
}