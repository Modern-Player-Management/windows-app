using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;

namespace ModernPlayerManager.ViewModels.DataViewModels
{
    public class TeamViewModel : NotificationBase
    {
        public TeamViewModel(Team model) {
            Model = model;
        }

        public Team Model { get; set; }

        public string Id
        {
            get => Model.Id;
            set
            {
                Model.Id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => Model.Description;
            set
            {
                Model.Description = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get => Model.Image;
            set
            {
                Model.Image = value;
                OnPropertyChanged();
            }
        }

        public bool IsCurrentUserManager
        {
            get => Model.IsCurrentUserManager; set
            {
                Model.IsCurrentUserManager = value;
                OnPropertyChanged();
            }
        }

        /*
        public User Manager { get; set; }
        
        public ObservableCollection<User> pPlayers { get; set; }
        public DateTime Created { get; set; }
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Game> Games { get; set; }
        */
    }
}