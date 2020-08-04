using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Models
{
    public class Discrepancy
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public int Type { get; set; }
        public string Reason { get; set; }
        public int DelayLength { get; set; }
    }
}
