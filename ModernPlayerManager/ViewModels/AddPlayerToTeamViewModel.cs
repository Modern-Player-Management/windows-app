using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.UI.Controls;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class AddPlayerToTeamViewModel : NotificationBase
    {
        private string search;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                OnPropertyChanged();
            }
        }

        public Team Team { get; set; }
        private User selectedUser;

        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                selectedUser = value;
                AddUserCommand.RaiseCanExecuteChanged();
            }
        }

        public ICommand SearchUserCommand { get; private set; }
        public AddPlayerToTeamCommand AddUserCommand { get; private set; }
        public ObservableCollection<User> SearchResult { get; set; } = new ObservableCollection<User>();
        public IUserApi UserApi = RestService.For<IUserApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });
        public ITeamApi TeamApi = RestService.For<ITeamApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });


        public AddPlayerToTeamViewModel(Team team) {
            SearchUserCommand = new SearchUserCommand(this);
            Team = team;
            AddUserCommand = new AddPlayerToTeamCommand(this);
        }

        public async void SearchUser() {
            SearchResult.Clear();
            (await UserApi.SearchUser(Search)).ForEach(SearchResult.Add);
        }

        public async void AddUser() {
            if (SelectedUser == null) return;
            try{
                await TeamApi.AddPlayerToTeam(Team.Id, SelectedUser.Id);
            }
            catch(Exception e) {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = e.Message,
                    CloseButtonText = "Ok"
                };
                await dialog.ShowAsync();
            }
        }
    }
}
