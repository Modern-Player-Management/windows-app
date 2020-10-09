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
        private bool loading = false;

        private string username;

        public string Username
        {
            get => username;
            set
            {
                username = value;
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

        public RelayCommand ClickLoginCommand { get; set; }
        public RelayCommand NavigateToRegisterCommand { get; set; }

        public LoginViewModel() {
            ClickLoginCommand = new RelayCommand(Login, CanLogin);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister, () => true);
            CheckStoredCredentials();
        }

        public bool RememberMe { get; set; }
        public bool CanLogin() => Username?.Length > 0 && Password?.Length > 0;

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

        public void CheckStoredCredentials()
        {
            try
            {
                var vault = new Windows.Security.Credentials.PasswordVault();
                var credentialList = vault.FindAllByResource("Modern Player Manager");

                if (credentialList.Count == 0)
                {
                    return;
                }

                var credential = credentialList[0];
                credential.RetrievePassword();
                Username = credential.UserName;
                Password = credential.Password;

                Login();
            }
            catch (Exception e)
            {

            }
        }

        public async void Login() {
            Loading = true;
            var api = RestService.For<ILoginApi>(new HttpClient()
                {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

            var dto = new LoginDTO() {Username = this.Username, Password = this.Password};

            try {
                var loggedUser = await api.Login(dto);
                AuthenticatedHttpClientHandler.Token = loggedUser.Token;
                AuthenticatedHttpClientHandler.UserId = loggedUser.Id;

                if (RememberMe)
                {
                    var vault = new Windows.Security.Credentials.PasswordVault();
                    vault.Add(new Windows.Security.Credentials.PasswordCredential(
                        "Modern Player Manager", Username, Password));
                }

                (Window.Current.Content as Frame)?.Navigate(typeof(MainPage));
            }
            catch (Exception e) {
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