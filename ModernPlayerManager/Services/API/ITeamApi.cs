using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Annotations;
using ModernPlayerManager.Models;
using Refit;

namespace ModernPlayerManager.Services.API
{
    [Headers("Authorization: Bearer")]
    public interface ITeamApi
    {
        [Get("/api/Teams")]
        Task<List<Team>> GetTeams();

        [Post("/api/Teams")]
        Task<Team> CreateTeam([Body] Team team);
    }
}
