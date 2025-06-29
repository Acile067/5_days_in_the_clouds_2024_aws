using _5_days_in_the_clouds_2024.Domain.Contracts;
using _5_days_in_the_clouds_2024.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Player.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, CreatePlayerResponse>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public CreatePlayerCommandHandler(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<CreatePlayerResponse> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var playerEntity = new Domain.Entities.Player
            {
                Id = Guid.NewGuid().ToString(),
                Nickname = request.Nickname,
                Wins = 0,
                Losses = 0,
                Elo = 0, 
                HoursPlayed = 0,
                Team = null, 
                RatingAdjustment = null 
            };

            var ret = await _playerRepository.CreateAsync(playerEntity);

            return _mapper.Map<CreatePlayerResponse>(ret);
        }
    }
}
