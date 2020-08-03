using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels.Commands;

namespace ModernPlayerManager.ViewModels
{
    public class LoginViewModel : NotificationBase
    {
        private bool _loading = false;
        public string Username { get; set; }
        public string Password { get; set; }
        
        public ClickLoginCommand ClickLoginCommand { get; set; }

        public LoginViewModel() {
            ClickLoginCommand = new ClickLoginCommand(this);
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

        public void Login() {
            Loading = true;
        }
    }
}
