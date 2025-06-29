using _5_days_in_the_clouds_2024.Application.Common.Exceptions;
using _5_days_in_the_clouds_2024.Domain.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Player.Quieries.GetPlayerById
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, GetPlayerByIdResponse>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;
        public GetPlayerByIdQueryHandler(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }
        public async Task<GetPlayerByIdResponse> Handle(GetPlayerByIdQuery request, CancellationToken cancellationToken)
        {
            var player = await _playerRepository.GetByIdAsync(request.Id);
            if (player == null)
            {
                throw new NotFoundException("Player", request.Id);
            }
            return _mapper.Map<GetPlayerByIdResponse>(player);
        }
    }
}
