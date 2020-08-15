using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class Participation : NotificationBase
    {
        private bool confirmed { get; set; }
        private string userId { get; set; }
        private string username { get; set; }

        public string UserId {
            get => userId;
            set {
                userId = value;
                OnPropertyChanged();
            }
        }

        public string Username {
            get => username;
            set {
                username = value;
                OnPropertyChanged();
            }
        }

        public bool Confirmed {
            get => confirmed;
            set {
                confirmed = value;
                OnPropertyChanged();
            }
        }
    }
}
