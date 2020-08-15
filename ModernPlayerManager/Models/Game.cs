using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private GameResult win;
        private ObservableCollection<PlayersStat> playersStats;

        public enum GameResult { Win, Loss, Draw }

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

        public ObservableCollection<PlayersStat> PlayersStats {
            get => playersStats;
            set {
                playersStats = value;
                OnPropertyChanged();
            }
        }

        public GameResult Win {
            get => win;
            set {
                win = value;
                OnPropertyChanged();
            }
        }

        public DateTime Date {
            get => date;
            set {
                date = value;
                OnPropertyChanged();
            }
        }



    }
}
