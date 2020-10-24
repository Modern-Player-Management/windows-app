using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services;
using ModernPlayerManager.Services.API;
using ModernPlayerManager.Services.DTO;
using ModernPlayerManager.ViewModels.Commands;
using Refit;

namespace ModernPlayerManager.ViewModels
{
    public class EditTeamViewModel : NotificationBase
    {
        private Team team;

        public Team Team
        {
            get => team;
            set
            {
                team = value;
                OnPropertyChanged();
            }
        }

        public ITeamApi TeamApi = RestService.For<ITeamApi>(MpmHttpClient.Instance);


        public EditTeamViewModel(Team team) {
            this.team = team;
            UpdateTeamCommand = new AsyncCommand(UpdateTeam);
        }

        public AsyncCommand UpdateTeamCommand { get; private set; }

        public async Task UpdateTeam() {
            await TeamApi.UpdateTeam(Team.Id, new UpdateTeamDTO()
            {
                Name = Team.Name,
                Description = Team.Description
            });
        }
    }
}
