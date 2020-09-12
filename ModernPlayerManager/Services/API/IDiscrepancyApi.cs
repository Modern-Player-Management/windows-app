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
    public interface IDiscrepancyApi
    {
        [Delete("/api/Discrepancies/{discrepancyId}")]
        Task DeleteDiscrepancy(string discrepancyId);

        [Put("/api/Discrepancies/{discrepancyId}")]
        Task UpdateDiscrepancy(string discrepancyId, [Body] DiscrepancyDTO dto);
    }
}
