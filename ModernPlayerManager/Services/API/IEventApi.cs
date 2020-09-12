using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Services.DTO;
using Refit;

namespace ModernPlayerManager.Services.API
{
    [Headers("Authorization: Bearer")]
    public interface IEventApi
    {
        [Post("/api/Events/{eventId}/discrepancies")]
        Task CreateDiscrepancy(string eventId, [Body] CreateDiscrepancyDTO dto);
    }
}
