using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class ClickRegisterCommand : ICommand
    {
        public RegisterViewModel ViewModel { get; set; }

        public ClickRegisterCommand(RegisterViewModel viewModel) {
            ViewModel = viewModel;
        }

        public bool CanExecute(object parameter) {
            return ViewModel.Username?.Length > 0 && ViewModel.Email?.Length > 0 && ViewModel.Password?.Length > 0;
        }

        public void Execute(object parameter) {
            ViewModel.Register();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
