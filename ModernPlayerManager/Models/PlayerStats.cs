using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class PlayersStat : NotificationBase
    {
        private string player { get; set; }
        private int goals { get; set; }
        private int saves { get; set; }
        private int shots { get; set; }
        private int assists { get; set; }
        private int score { get; set; }
        private string gameId { get; set; }
        private int goalShots { get; set; }
        private string id { get; set; }
        private DateTime created { get; set; }
    }
}
