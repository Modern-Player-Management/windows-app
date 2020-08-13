using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class AddPlayerToTeamCommand : ICommand
    {
        public AddPlayerToTeamViewModel ViewModel { get; set; }

        public AddPlayerToTeamCommand(AddPlayerToTeamViewModel viewModel) {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter) {
            return ViewModel.SelectedUser != null;
        }

        public void Execute(object parameter) {
            ViewModel.AddUser();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
