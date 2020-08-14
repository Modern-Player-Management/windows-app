using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{

    public class Game : NotificationBase
    {
        private string id;
        private string name;
        private DateTime date;
        private int win;
        private List<PlayersStat> playersStats;

        public string Id {
            get => id;
            set {
                id = value;
                OnPropertyChanged();
            }
        }

        public string Name {
            get => name;
            set {
                name = value;
                OnPropertyChanged();
            }
        }

    }
}
