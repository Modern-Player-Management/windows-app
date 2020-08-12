using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    class OpenAddPlayerToTeamDialogCommand : ICommand
    {
        private TeamViewModel viewModel;

        public OpenAddPlayerToTeamDialogCommand(TeamViewModel viewModel) {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            viewModel.AddPlayerToTeam();
        }

        public event EventHandler CanExecuteChanged;
    }
}
