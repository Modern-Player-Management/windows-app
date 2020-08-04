using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Models
{

    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int Win { get; set; }
        public List<PlayersStat> PlayersStats { get; set; }
    }
}
