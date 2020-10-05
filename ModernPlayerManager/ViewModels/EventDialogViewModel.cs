using ModernPlayerManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Services.DTO;

namespace ModernPlayerManager.ViewModels
{
    public class EventDialogViewModel : NotificationBase
    {
        public UpsertEventDTO EventDto { get; private set; }
        public DialogMode Mode { get; private set; }

        private string typeName;
        public string Type {
            get => typeName;
            set {
                typeName = value;
                Enum.TryParse<Event.EventType>(typeName, out type);

            }
        }

        public string Title => Mode.ToString() + " Event";

        private Event.EventType type;
        public List<string> TypeValues => Enum.GetNames(typeof(Event.EventType)).ToList();

        public EventDialogViewModel()
        {
            Mode = DialogMode.Create;
            this.EventDto = new UpsertEventDTO()
            {
                Start = DateTime.Now,
                End = DateTime.Now
            };
        }


        public EventDialogViewModel(Event evt)
        {
            this.EventDto = new UpsertEventDTO()
            {
                Description = evt.Description,
                End = evt.End,
                Name = evt.Name,
                Start = evt.Start,
                Type = evt.Type
            };
            Mode = DialogMode.Edit;
        }
    }
}
