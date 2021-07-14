using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace contagiaDOSAPI.Models.Entities
{
    public partial class Round
    {
        public Round()
        {
            Group = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string Leader { get; set; }
        public bool? Psychowin { get; set; }
        public int GameId { get; set; }
        public int? RoundNumber { get; set; }

        public virtual Game Game { get; set; }
        public virtual ICollection<Group> Group { get; set; }
    }
}
