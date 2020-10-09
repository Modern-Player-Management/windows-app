using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace ModernPlayerManager.Services.API
{
    [Headers("Authorization: Bearer")]
    public interface IGameApi
    {
        [Delete("/api/Games/{gameId}")]
        Task DeleteGame(string gameId);
    }
}
