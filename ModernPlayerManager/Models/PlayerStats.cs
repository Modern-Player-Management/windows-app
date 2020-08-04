using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Models
{
    public class PlayersStat
    {
        public string Player { get; set; }
        public int Goals { get; set; }
        public int Saves { get; set; }
        public int Shots { get; set; }
        public int Assists { get; set; }
        public int Score { get; set; }
        public string GameId { get; set; }
        public int GoalShots { get; set; }
        public string Id { get; set; }
        public DateTime Created { get; set; }
    }
}
