using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Dialogs;
using ModernPlayerManager.Models;
using ModernPlayerManager.ViewModels.Commands;

namespace ModernPlayerManager.ViewModels
{
    public class GameListItemViewModel : NotificationBase
    {
        private Game game;

        public bool IsUserTeamManager { get; set; }

        public Game Game
        {
            get => game;
            set
            {
                game = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand ShowGameDetailsCommand;
        public RelayCommand DeleteGameCommand;

        public GameListItemViewModel(Game game, bool isUserTeamManager) {
            this.game = game;
            IsUserTeamManager = isUserTeamManager;

            ShowGameDetailsCommand = new RelayCommand(ShowGameDetails);
            DeleteGameCommand = new RelayCommand(DeleteGame, CanDelete);
        }

        public bool CanDelete() => IsUserTeamManager;

        public async void ShowGameDetails() {
            var dialog = new GameDialog(Game);
            await dialog.ShowAsync();
        }

        public async void DeleteGame() {

        }
    }
}
