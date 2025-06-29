using _5_days_in_the_clouds_2024.Application.Common.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Team.Quieries.GetTeamById
{
    public class GetTeamByIdQuery : IRequest<GetTeamByIdResponse>
    {
        public string Id { get; set; }
    }

    public class GetTeamByIdResponse : IMapFrom<Domain.Entities.Team>
    {
        public string Id { get; set; }
        public string TeamName { get; set; }
        public List<Domain.Entities.Player> Players { get; set; }
    }
}
