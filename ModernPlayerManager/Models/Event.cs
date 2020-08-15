using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class Event : NotificationBase
    {
        private string id;
        private string name;
        private string description;
        private DateTime start;
        private DateTime end;
        private EventType type;
        private ObservableCollection<Participation> participations;
        private ObservableCollection<Discrepancy> discrepancies;
        private bool currentHasConfirmed;

        public enum EventType
        {
            Scrim,
            Meeting,
            Tournament,
            Coaching
        }

        public ObservableCollection<Discrepancy> Discrepancies
        {
            get => discrepancies;
            set
            {
                discrepancies = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Participation> Participations
        {
            get => participations;
            set
            {
                participations = value;
                OnPropertyChanged();
            }
        }

        public EventType Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged();
            }
        }

        public string Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public bool CurrentHasConfirmed
        {
            get => currentHasConfirmed;
            set
            {
                currentHasConfirmed = value;
                OnPropertyChanged();
            }
        }

        public DateTime Start
        {
            get => start;
            set
            {
                start = value;
                OnPropertyChanged();
            }
        }

        public DateTime End
        {
            get => end;
            set
            {
                end = value;
                OnPropertyChanged();
            }
        }
    }
}