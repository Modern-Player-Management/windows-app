using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class RelayCommandParams<T> : ICommand
    {
        private Action<T> execute;
        private Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged() {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public RelayCommandParams(Action<T> execute, Func<T, bool> canExecute = null) {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(T parameter) {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(T parameter) {
            this.execute(parameter);
        }

        #region Explicit implementations

        public bool CanExecute(object parameter) {
            return this.canExecute == null || this.canExecute((T) parameter);
        }

        public void Execute(object parameter) {
            this.execute((T) parameter);
        }

        #endregion
    }
}