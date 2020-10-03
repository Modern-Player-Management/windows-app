using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;

namespace ModernPlayerManager.Services.DTO
{
    public class UpsertEventDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public Event.EventType Type { get; set; }
    }
}
