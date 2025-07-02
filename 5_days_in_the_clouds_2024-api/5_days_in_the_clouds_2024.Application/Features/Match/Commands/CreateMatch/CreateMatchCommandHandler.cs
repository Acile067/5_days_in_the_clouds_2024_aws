using _5_days_in_the_clouds_2024.Domain.Contracts;
using _5_days_in_the_clouds_2024.Domain.Entities;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Match.Commands.CreateMatch
{
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, CreateMatchResponse>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;
        private readonly IMatchUploaderService _matchUploaderService;
        public CreateMatchCommandHandler(IMatchRepository matchRepository, IMapper mapper, ITeamRepository teamRepository, IMatchUploaderService matchUploaderService)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
            _teamRepository = teamRepository;
            _matchUploaderService = matchUploaderService;
        }

        public async Task<CreateMatchResponse> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var team1 = await _teamRepository.GetByIdAsync(request.Team1Id);
            var team2 = await _teamRepository.GetByIdAsync(request.Team2Id);

            double team1AvgElo = CalculateTeamELO(team1.Players);
            double team2AvgElo = CalculateTeamELO(team2.Players);

            bool isDraw = string.IsNullOrWhiteSpace(request.WinningTeamId);

            if (isDraw)
            {
                UpdateTeamPlayers(team1.Players, 0.5, request.Duration, team2AvgElo);
                UpdateTeamPlayers(team2.Players, 0.5, request.Duration, team1AvgElo);
            }
            else
            {
                double team1Score = request.WinningTeamId == request.Team1Id ? 1 : 0;
                double team2Score = 1 - team1Score;

                UpdateTeamPlayers(team1.Players, team1Score, request.Duration, team2AvgElo);
                UpdateTeamPlayers(team2.Players, team2Score, request.Duration, team1AvgElo);
            }

            await _teamRepository.UpdateAsync(team1);
            await _teamRepository.UpdateAsync(team2);

            var match = new _5_days_in_the_clouds_2024.Domain.Entities.Match
            {
                Id = Guid.NewGuid().ToString(),
                Team1Id = request.Team1Id,
                Team2Id = request.Team2Id,
                WinningTeamId = request.WinningTeamId,
                Duration = request.Duration
            };

            var ret = await _matchRepository.CreateAsync(match);

            await _matchUploaderService.UploadMatchAsync(match);

            return _mapper.Map<CreateMatchResponse>(ret);
        }

        private double CalculateTeamELO(IEnumerable<_5_days_in_the_clouds_2024.Domain.Entities.Player> players)
        {
            return players.Average(player => player.Elo);
        }
        private void UpdateTeamPlayers(IEnumerable<_5_days_in_the_clouds_2024.Domain.Entities.Player> players, double score, int duration, double opponentElo)
        {
            foreach (var player in players)
            {
                double expectedScore = CalculateExpectedScore(player.Elo, opponentElo);

                player.HoursPlayed += duration;

                int k = CalculateK(player.HoursPlayed);

                player.Elo = (int)Math.Round(player.Elo + k * (score - expectedScore));

                player.RatingAdjustment = k;

                if (score == 1)
                {
                    player.Wins++;
                }
                else if (score == 0)
                {
                    player.Losses++;
                }
            }
        }
        private int CalculateK(int hoursPlayed)
        {
            if (hoursPlayed < 500) return 50;
            if (hoursPlayed < 1000) return 40;
            if (hoursPlayed < 3000) return 30;
            if (hoursPlayed < 5000) return 20;
            return 10;
        }
        private double CalculateExpectedScore(double r1, double r2)
        {
            return 1 / (1 + Math.Pow(10, (r2 - r1) / 400));
        }
    }
}
