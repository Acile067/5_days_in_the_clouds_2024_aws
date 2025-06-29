using _5_days_in_the_clouds_2024.Domain.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Team.Commands.CreateTeam
{
    public class CreateTeamCommandValidatior : AbstractValidator<CreateTeamCommand>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;

        public CreateTeamCommandValidatior(IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;

            RuleFor(x => x.TeamName)
                .NotEmpty().WithMessage("Team name is required.")
                .MaximumLength(100).WithMessage("Team name must not exceed 100 characters.");

            RuleFor(x => x.TeamName)
                .MustAsync(async (teamName, cancellation) => !await _teamRepository.TeamExisist(teamName))
                .WithMessage("A team with this name already exists.")
                .When(x => !string.IsNullOrWhiteSpace(x.TeamName));

            RuleFor(x => x.Players)
                .MustAsync(async (players, cancellation) => await _playerRepository.ArePlayersValid(players))
                .WithMessage("One or more players do not exist in the database.");

            RuleFor(x => x.Players)
                .MustAsync(async (players, cancellation) => await _playerRepository.ArePlayersInAnotherTeam(players))
                .WithMessage("One or more players are in another team.");

            RuleFor(x => x.Players)
                .Must(players => players.Count == 5)
                .WithMessage("A team must have 5 players.");

            RuleFor(x => x.Players)
                .Must(players => players.Distinct().Count() == players.Count)
                .WithMessage("Duplicate player IDs are not allowed.");
        }
    }
}
