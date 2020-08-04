using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.ViewModels.DataViewModels;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class MainViewModel : NotificationBase
    {
        public ObservableCollection<TeamViewModel> Teams { get; set; }

        public ITeamApi Api = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});


        public async void FetchTeams() {
            Teams = new ObservableCollection<TeamViewModel>();
            (await Api.GetTeams()).Select(team => new TeamViewModel(team)).ToList().ForEach(Teams.Add);
        }

        public async void AddTeam() {
            var dialog = new CreateTeamDialog();
            var buttonClicked = await dialog.ShowAsync();
            if (buttonClicked == ContentDialogResult.Primary) {
                Teams.Add(dialog.ViewModel);
            }
        }
    }
}