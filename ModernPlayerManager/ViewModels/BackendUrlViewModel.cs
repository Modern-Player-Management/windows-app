using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ModernPlayerManager.Pages;
using ModernPlayerManager.ViewModels.Commands;

namespace ModernPlayerManager.ViewModels
{
    public class BackendUrlViewModel : NotificationBase
    {
        private string backendUrl;

        public string BackendUrl
        {
            get => backendUrl;
            set
            {
                backendUrl = value;
                OnPropertyChanged();
                ValidateCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand ValidateCommand;

        public BackendUrlViewModel() {
            ValidateCommand = new RelayCommand(Validate, () => BackendUrl?.Length > 0);
        }

        public void Validate() {
            ApplicationData.Current.LocalSettings.Values["backend_url"] = BackendUrl;
            (Window.Current.Content as Frame)?.Navigate(typeof(LoginPage));
        }
    }
}
