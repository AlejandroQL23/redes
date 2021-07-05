using System;
using System.Collections.Generic;

#nullable disable

namespace contagiaDOSAPI.Models.Entities
{
    public partial class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Leader { get; set; }
        public string Rol { get; set; }
        public string Infected { get; set; }
    }
}
