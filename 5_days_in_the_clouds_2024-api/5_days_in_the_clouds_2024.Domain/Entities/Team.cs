using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Domain.Entities
{
    public class Team
    {
        public string Id { get; set; } = string.Empty;
        public string TeamName { get; set; } = string.Empty;
        public List<Player> Players { get; set; } = new List<Player>();
    }
}
