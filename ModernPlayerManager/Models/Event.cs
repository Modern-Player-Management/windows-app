using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Models
{
    public class Event
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Type { get; set; }
        public List<Participation> Participations { get; set; }
        public List<Discrepancy> Discrepancies { get; set; }
        public bool CurrentHasConfirmed { get; set; }
    }
}
