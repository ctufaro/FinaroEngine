using System;
using System.Collections.Generic;
using System.Text;

namespace FinaroEngine.Library
{
    public class TeamPlayer
    {
        public int Id { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityLeagueId { get; set; }
        public int? EntityGroupRefId { get; set; }
        public string Name { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
    }
}
