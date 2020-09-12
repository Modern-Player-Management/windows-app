using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Services.DTO
{
    public class CreateDiscrepancyDTO
    {
        public int Type { get; set; }
        public string Reason { get; set; }
        public int DelayLength { get; set; }
    }


}
