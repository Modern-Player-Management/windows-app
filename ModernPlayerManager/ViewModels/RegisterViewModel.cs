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
using ModernPlayerManager.Services;
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

        public RelayCommand ClickRegisterCommand { get; set; }
        public RelayCommand NavigateToRegisterCommand { get; set; }

        public RegisterViewModel() {
            ClickRegisterCommand = new RelayCommand(Register, () => true);
            NavigateToRegisterCommand = new RelayCommand(NavigateToRegister, () => true);
        }

        private void NavigateToRegister() {
            (Window.Current.Content as Frame)?.Navigate(typeof(RegisterPage));
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

            var api = RestService.For<ILoginApi>(MpmHttpClient.Instance);

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