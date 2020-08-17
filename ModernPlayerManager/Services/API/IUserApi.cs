using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;
using Refit;

namespace ModernPlayerManager.Services.DTO
{
    [Headers("Authorization: Bearer")]
    public interface IUserApi
    {
        [Get("/api/Users/search")]
        Task<List<User>> SearchUser([Query("search")] string search);

        [Get("/api/Users/profile")]
        Task<UserProfile> GetUserProfile();
    }
}
