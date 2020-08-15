using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Models;
using ModernPlayerManager.ViewModels.Commands;

namespace ModernPlayerManager.ViewModels
{
    public class EventListItemViewModel : NotificationBase
    {
        private Event evt;

        public Event Event
        {
            get => evt;
            set
            {
                evt = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ShowEventDetailCommand { get; set; }


        public EventListItemViewModel(Event evt) {
            this.evt = evt;
            ShowEventDetailCommand = new RelayCommand(ShowEventDetails);
        }

        public async void ShowEventDetails()
        {
            var dialog = new EventDialog(evt);
            await dialog.ShowAsync();
        }
    }
}
