using _5_days_in_the_clouds_2024.Application.Common.Mappings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Player.Quieries.GetPlayers
{
    public class GetPlayersQuery : IRequest<List<GetPlayersResponse>>
    {

    }
    public class GetPlayersResponse : IMapFrom<Domain.Entities.Player>
    {
        public string Id { get; set; }
        public string Nickname { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Elo { get; set; }
        public int HoursPlayed { get; set; }
        public string? Team { get; set; }
        public int? RatingAdjustment { get; set; }
    }
}
