using System;
using System.Collections.Generic;

#nullable disable

namespace contagiaDOSAPI.Models.Entities
{
    public partial class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string State { get; set; }
    }
}
