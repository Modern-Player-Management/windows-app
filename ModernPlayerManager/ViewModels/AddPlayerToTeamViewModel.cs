using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public ICommand SearchUserCommand { get; private set; }
        public ObservableCollection<User> SearchResult { get; set; } = new ObservableCollection<User>();
        public IUserApi Api = RestService.For<IUserApi>(new HttpClient(new AuthenticatedHttpClientHandler())
            { BaseAddress = new Uri("https://api-mpm.herokuapp.com") });
        public AddPlayerToTeamViewModel() {
            SearchUserCommand = new SearchUserCommand(this);
        }

        public async void SearchUser() {
            SearchResult.Clear();
            (await Api.SearchUser(Search)).ForEach(SearchResult.Add);
        }


    }
}
