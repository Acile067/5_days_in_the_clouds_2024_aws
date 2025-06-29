using _5_days_in_the_clouds_2024.Domain.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Player.Quieries.GetPlayers
{
    public class GetPlayersQueryHandler : IRequestHandler<GetPlayersQuery, List<GetPlayersResponse>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public GetPlayersQueryHandler(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }
        public async Task<List<GetPlayersResponse>> Handle(GetPlayersQuery request, CancellationToken cancellationToken)
        {
            var players = await _playerRepository.GetAllAsync();
            return _mapper.Map<List<GetPlayersResponse>>(players);
        }
    }
}
