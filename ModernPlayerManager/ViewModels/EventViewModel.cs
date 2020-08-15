using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;

namespace ModernPlayerManager.ViewModels
{
    public class EventViewModel : NotificationBase
    {
        private Event evt;

        public EventViewModel(Event evt) {
            this.evt = evt;
        }

        public Event Event
        {
            get => evt;
            set
            {
                evt = value;
                OnPropertyChanged();
            }
        }
    }
}
