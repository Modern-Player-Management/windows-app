using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class Team : NotificationBase
    {
        private string id;
        private string name;
        private string description;
        private string image;
        private User manager;
        private bool isCurrentUserManager;
        private List<User> players;
        private DateTime created;
        private List<Event> events;
        private List<Game> games;


        public string Id {
            get => id;
            set {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name {
            get => name;
            set {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description {
            get => description;
            set {
                description = value;
                OnPropertyChanged();
            }
        }

        public string Image {
            get => image;
            set {
                image = value;
                OnPropertyChanged();
            }
        }

        public bool IsCurrentUserManager {
            get => isCurrentUserManager;
            set {
                isCurrentUserManager = value;
                OnPropertyChanged();
            }
        }
    }
}
