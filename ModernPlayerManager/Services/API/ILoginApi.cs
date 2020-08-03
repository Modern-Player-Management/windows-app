using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Services.DTO;
using Refit;

namespace ModernPlayerManager.Services.API
{
    public interface ILoginApi
    {
        [Post("/api/Auth/authenticate")]
        Task<LoggedUserDTO> Login([Body] LoginDTO dto);

        [Post("/api/Auth/register")]
        Task Register([Body] RegisterDTO dto);
    }
}
