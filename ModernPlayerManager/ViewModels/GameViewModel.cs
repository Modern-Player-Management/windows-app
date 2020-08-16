using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;

namespace ModernPlayerManager.ViewModels
{
    public class GameViewModel : NotificationBase
    {
        private Game game;

        public Game Game
        {
            get => game;
            set
            {
                game = value;
                OnPropertyChanged();
            }
        }

        public GameViewModel(Game game) {
            this.game = game;
        }
    }
}
