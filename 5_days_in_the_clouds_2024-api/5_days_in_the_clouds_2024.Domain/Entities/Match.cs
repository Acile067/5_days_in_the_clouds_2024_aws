using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Domain.Entities
{
    public class Match
    {
        public string Id { get; set; } = string.Empty;
        public string Team1Id { get; set; } = string.Empty;
        public string Team2Id { get; set; } = string.Empty;
        public string? WinningTeamId { get; set; }
        public int Duration { get; set; }
    }
}
