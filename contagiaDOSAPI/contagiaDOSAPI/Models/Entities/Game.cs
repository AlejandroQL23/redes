using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace contagiaDOSAPI.Models.Entities
{
    public partial class Game
    {
        public Game()
        {
            Player = new HashSet<Player>();
        }

        public int GameId { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

        public virtual Round Round { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}
