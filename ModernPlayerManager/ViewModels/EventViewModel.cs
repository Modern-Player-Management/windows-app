using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class EventViewModel : NotificationBase
    {
        private Event evt;

        public EventViewModel(Event evt)
        {
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


        public IDiscrepancyApi TeamApi = RestService.For<IDiscrepancyApi>(
            new HttpClient(new AuthenticatedHttpClientHandler())
                {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});


        public async Task DeleteDiscrepancy(Discrepancy discrepancy)
        {
            try
            {
                await TeamApi.DeleteDiscrepancy(discrepancy.Id);
                evt.Discrepancies.Remove(discrepancy);
            }
            catch (Exception e)
            {
                var dialog = new MessageDialog(e.Message
                );
                await dialog.ShowAsync();
            }
        }
    }
}