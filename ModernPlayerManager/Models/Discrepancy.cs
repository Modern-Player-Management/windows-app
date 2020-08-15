using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernPlayerManager.ViewModels;

namespace ModernPlayerManager.Models
{
    public class Discrepancy : NotificationBase
    {
        private string id { get; set; }
        private string userId { get; set; }
        private string username { get; set; }
        private DiscrepancyType type { get; set; }
        private string reason { get; set; }
        private int delayLength { get; set; }

        public enum DiscrepancyType
        {
            Absence,
            Delay
        }

        public string Id {
            get => id;
            set {
                id = value;
                OnPropertyChanged();
            }
        }

        public int DelayLength {
            get => delayLength;
            set {
                delayLength = value;
                OnPropertyChanged();
            }
        }

        public string Reason {
            get => reason;
            set {
                reason = value;
                OnPropertyChanged();
            }
        }

        public DiscrepancyType Type {
            get => type;
            set {
                type = value;
                OnPropertyChanged();
            }
        }

        public string UserId {
            get => userId;
            set {
                userId = value;
                OnPropertyChanged();
            }
        }

        public string Username {
            get => username;
            set {
                username = value;
                OnPropertyChanged();
            }
        }
    }
}
