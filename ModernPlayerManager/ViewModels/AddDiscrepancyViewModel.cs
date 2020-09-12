using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class AddDiscrepancyViewModel : NotificationBase
    {
        public string Reason { get; set; }

        private string typeName;
        public string Type
        {
            get => typeName;
            set
            {
                typeName = value;
                DelayLengthShown = (typeName == "Delay");
                Enum.TryParse<Discrepancy.DiscrepancyType>(typeName,out type);

            }
        }

        private Discrepancy.DiscrepancyType type;
        private bool delayLengthShown = false;

        public bool DelayLengthShown
        {
            get => delayLengthShown;
            set
            {
                delayLengthShown = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CreateDiscrepancyCommand { get; private set; }

        public uint DelayLength { get; set; }
        public List<string> TypeValues => Enum.GetNames(typeof(Discrepancy.DiscrepancyType)).ToList();
        public CreateDiscrepancyDTO Dto { get; private set; }

        public AddDiscrepancyViewModel() {
            CreateDiscrepancyCommand = new RelayCommand(CreateDiscrepancy);
        }

        private void CreateDiscrepancy() {
            Dto = new CreateDiscrepancyDTO()
            {
                Reason = Reason,
                Type = (int) this.type,
                DelayLength = (int) DelayLength
            };
        }
    }
}
