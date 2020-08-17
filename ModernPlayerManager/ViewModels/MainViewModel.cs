using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Models;
using ModernPlayerManager.Pages;
using ModernPlayerManager.Services.API;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class MainViewModel : NotificationBase
    {
        public ObservableCollection<Team> Teams { get; set; } = new ObservableCollection<Team>();

        public Team SelectedTeamViewModel { get; set; }

        public ITeamApi Api = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            {BaseAddress = new Uri("https://api-mpm.herokuapp.com")});

        public Frame ContentFrame { get;  set; }


        public async void FetchTeams() {
            Teams.Clear();
            (await Api.GetTeams()).ForEach(Teams.Add);
        }

        public void NavigateToUserProfile() {
            ContentFrame.Navigate(typeof(UserProfilePage));
        }

        public async void AddTeam() {
            var dialog = new CreateTeamDialog();
            var buttonClicked = await dialog.ShowAsync();
            if (buttonClicked == ContentDialogResult.Primary) {
                Teams.Add(dialog.Team);
            }
        }
    }
}