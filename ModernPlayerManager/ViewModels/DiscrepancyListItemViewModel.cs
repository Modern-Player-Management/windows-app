using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class DiscrepancyListItemViewModel : NotificationBase
    {
        private Discrepancy discrepancy;

        public DiscrepancyListItemViewModel(Discrepancy discrepancy)
        {
            this.Discrepancy = discrepancy;
        }


        public Discrepancy Discrepancy
        {
            get => discrepancy;
            set
            {
                discrepancy = value;
                OnPropertyChanged();
            }
        }


    }
}
