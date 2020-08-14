using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<User> players;
        private DateTime created;
        private ObservableCollection<Event> events;
        private ObservableCollection<Game> games;

        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged();
            }
        }

        public DateTime Created
        {
            get => created;
            set
            {
                created = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Players
        {
            get => players;
            set
            {
                players = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Game> Games {
            get => games;
            set {
                games = value;
                OnPropertyChanged();
            }
        }

        public User Manager
        {
            get => manager;
            set
            {
                manager = value;
                OnPropertyChanged();
            }
        }

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
