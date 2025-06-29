using _5_days_in_the_clouds_2024.Application.Common.Mappings;
using _5_days_in_the_clouds_2024.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Team.Commands.CreateTeam
{
    public class CreateTeamCommand : IRequest<CreateTeamResponse>
    {
        public string TeamName { get; set; } = string.Empty;
        public List<string> Players { get; set; } = new List<string>();
    }
    public class CreateTeamResponse : IMapFrom<Domain.Entities.Team>
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public List<Domain.Entities.Player> Players { get; set; } = new List<Domain.Entities.Player>();
    }
}
