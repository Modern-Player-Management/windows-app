using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class ClickLoginCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; }

        public ClickLoginCommand(LoginViewModel loginViewModel) {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter) {
            return true;
        }

        public void Execute(object parameter) {
            LoginViewModel.Login();
        }

        public event EventHandler CanExecuteChanged;
    }
}
