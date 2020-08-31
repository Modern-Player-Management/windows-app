using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.Models;

namespace ModernPlayerManager.ViewModels
{
    public class EditUserViewModel : NotificationBase
    {
        private UserProfile userProfile;

        public UserProfile UserProfile
        {
            get => userProfile;
            set
            {
                userProfile = value;
                OnPropertyChanged();
            }
        }

        public EditUserViewModel(UserProfile userProfile)
        {
            this.userProfile = userProfile;
        }
    }
}
