using _5_days_in_the_clouds_2024.Domain.Contracts;
using _5_days_in_the_clouds_2024.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Team.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public CreateTeamCommandHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        public async Task<CreateTeamResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var playerIds = request.Players ?? new List<string>();

            var playersInDb = await _teamRepository.GetPlayersByGuidsAsync(playerIds);

            var teamEntity = new Domain.Entities.Team
            {
                Id = Guid.NewGuid().ToString(),
                TeamName = request.TeamName,
                Players = playersInDb
            };

            foreach (var player in playersInDb)
            {
                player.Team = teamEntity.Id;
                player.RatingAdjustment = 50;
            }

            var ret = await _teamRepository.CreateAsync(teamEntity);

            return _mapper.Map<CreateTeamResponse>(ret);
        }
    }
}
