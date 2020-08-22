using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.UI.Controls.TextToolbarSymbols;
using ModernPlayerManager.Annotations;
using ModernPlayerManager.Models;
using ModernPlayerManager.Services.DTO;
using Refit;

namespace ModernPlayerManager.Services.API
{
    [Headers("Authorization: Bearer")]
    public interface ITeamApi
    {
        [Get("/api/Teams")]
        Task<List<Team>> GetTeams();

        [Get("/api/Teams/{id}")]
        Task<Team> GetTeam([AliasAs("id")] string teamId);

        [Post("/api/Teams")]
        Task<Team> CreateTeam([Body] Team team);

        [Delete("/api/Teams/{id}")]
        Task DeleteTeam([AliasAs("id")] string teamId);

        [Post("/api/Teams/{teamId}/player/{playerId}")]
        Task AddPlayerToTeam([AliasAs("teamId")] string teamId, [AliasAs("playerId")] string playerId);

        [Put("/api/Teams/{id}")]
        Task UpdateTeam([AliasAs("id")] string teamId, [Body] UpdateTeamDTO dto);

        [Get("/api/Teams/{id}/stats")]
        Task<List<PlayersStat>> GetStats([AliasAs("id")] string teamId);

    }
}
