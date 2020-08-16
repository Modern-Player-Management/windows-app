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

        public int GoalShots {
            get => goalShots;
            set {
                goalShots = value;
                OnPropertyChanged();
            }
        }

        public string GameId {
            get => gameId;
            set {
                gameId = value;
                OnPropertyChanged();
            }
        }


        public int Score {
            get => score;
            set {
                score = value;
                OnPropertyChanged();
            }
        }

        public int Assists {
            get => assists;
            set {
                assists = value;
                OnPropertyChanged();
            }
        }

        public int Shots {
            get => shots;
            set {
                shots = value;
                OnPropertyChanged();
            }
        }

        public int Saves {
            get => saves;
            set {
                saves = value;
                OnPropertyChanged();
            }
        }


        public int Goals {
            get => goals;
            set {
                goals = value;
                OnPropertyChanged();
            }
        }


        public string Player {
            get => player;
            set {
                player = value;
                OnPropertyChanged();
            }
        }


        public string Id {
            get => id;
            set {
                id = value;
                OnPropertyChanged();
            }
        }

    }
}
