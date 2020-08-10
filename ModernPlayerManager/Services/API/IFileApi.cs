using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace ModernPlayerManager.Services.API
{
    [Headers("Authorization: Bearer")]
    public interface IFileApi
    {
        [Get("/api/Files/{id}")]
        Task<HttpContent> GetFile(string id);
    }
}
