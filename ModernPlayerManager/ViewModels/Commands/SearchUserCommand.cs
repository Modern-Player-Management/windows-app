using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class SearchUserCommand : ICommand
    {
        private AddPlayerToTeamViewModel viewModel;

        public SearchUserCommand(AddPlayerToTeamViewModel viewModel) {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            viewModel.SearchUser();
        }

        public event EventHandler CanExecuteChanged;
    }
}
