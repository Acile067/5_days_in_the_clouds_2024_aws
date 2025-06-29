using _5_days_in_the_clouds_2024.Application.Common.Exceptions;
using _5_days_in_the_clouds_2024.Domain.Contracts;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Team.Quieries.GetTeamById
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, GetTeamByIdResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        public GetTeamByIdQueryHandler(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        public async Task<GetTeamByIdResponse> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.GetByIdAsync(request.Id);
            if (team == null)
            {
                throw new NotFoundException("Team", request.Id);
            }
            return _mapper.Map<GetTeamByIdResponse>(team);
        }
    }
}
