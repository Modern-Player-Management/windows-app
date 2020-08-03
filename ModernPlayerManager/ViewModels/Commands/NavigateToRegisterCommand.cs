using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class NavigateToRegisterCommand : ICommand
    {
        public LoginViewModel ViewModel;

        public NavigateToRegisterCommand(LoginViewModel viewModel) {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            ViewModel.NavigateToRegister();
        }

        public event EventHandler CanExecuteChanged;
    }
}
