using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class User : NotificationBase
    {
        private string id { get; set; }
        private string username { get; set; }
        private string image { get; set; }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }
    }
}
