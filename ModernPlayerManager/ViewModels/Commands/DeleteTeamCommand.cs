﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModernPlayerManager.ViewModels.Commands
{
    public class DeleteTeamCommand : ICommand
    {
        private readonly TeamViewModel teamViewModel;

        public DeleteTeamCommand(TeamViewModel teamViewModel) {
            this.teamViewModel = teamViewModel;
        }

        public bool CanExecute(object parameter) {
            return teamViewModel.Team?.IsCurrentUserManager ?? false;
        }

        public void Execute(object parameter) {
            teamViewModel.DeleteTeam();
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
