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
using Windows.Storage;
using ModernPlayerManager.Services;

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

        public RelayCommand SearchUserCommand { get; private set; }
        public RelayCommand AddUserCommand { get; private set; }
        public ObservableCollection<User> SearchResult { get; set; } = new ObservableCollection<User>();
        public IUserApi UserApi = RestService.For<IUserApi>(MpmHttpClient.Instance);
        public ITeamApi TeamApi = RestService.For<ITeamApi>(MpmHttpClient.Instance);


        public AddPlayerToTeamViewModel(Team team) {
            SearchUserCommand = new RelayCommand(SearchUser, () => true);
            Team = team;
            AddUserCommand = new RelayCommand(AddUser, CanAddPlayer);
        }

        private bool CanAddPlayer() => SelectedUser != null;

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
