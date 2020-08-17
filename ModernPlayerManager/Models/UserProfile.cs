using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class UserProfile : NotificationBase
    {
        private string username { get; set; }
        private string image { get; set; }
        private string email { get; set; }
        private string calendarSecret { get; set; }

        public string Image {
            get => image; set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public string Email {
            get => email; set {
                email = value;
                OnPropertyChanged();
            }
        }

        public string CalendarSecret {
            get => calendarSecret; set {
                calendarSecret = value;
                OnPropertyChanged();
            }
        }

        public string Username {
            get => username; set {
                username = value;
                OnPropertyChanged();
            }
        }
    }
}
