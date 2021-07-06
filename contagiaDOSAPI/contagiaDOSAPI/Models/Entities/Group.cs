using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace contagiaDOSAPI.Models.Entities
{
    public partial class Group
    {
        public int Id { get; set; }
        public int? PlayerId { get; set; }
        public string Name { get; set; }

        public virtual Player Player { get; set; }
    }
}
