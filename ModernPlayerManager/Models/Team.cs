using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernPlayerManager.Models
{
    public class Team
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public User Manager { get; set; }
        public bool IsCurrentUserManager { get; set; }
        public List<User> pPlayers { get; set; }
        public DateTime Created { get; set; }
        public List<Event> Events { get; set; }
        public List<Game> Games { get; set; }
    }
}
