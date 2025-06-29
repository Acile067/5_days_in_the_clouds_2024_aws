using _5_days_in_the_clouds_2024.Domain.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Match.Commands.CreateMatch
{
    public class CreateMatchCommandValidator : AbstractValidator<CreateMatchCommand>
    {
        private readonly ITeamRepository _teamRepository;
        public CreateMatchCommandValidator(ITeamRepository teamRepository)
        { 
            _teamRepository = teamRepository;

            RuleFor(x => x.Team1Id)
                .NotEmpty().WithMessage("Team 1 ID is required.")
                .MaximumLength(100).WithMessage("Team 1 ID must not exceed 100 characters.");

            RuleFor(x => x.Team1Id)
                .MustAsync(async (team1Id, cancellation) => await _teamRepository.GetByIdAsync(team1Id) != null)
                .WithMessage("Team 1 does not exist.")
                .When(x => !string.IsNullOrWhiteSpace(x.Team1Id));

            RuleFor(x => x.Team2Id)
                .NotEmpty().WithMessage("Team 2 ID is required.")
                .MaximumLength(100).WithMessage("Team 2 ID must not exceed 100 characters.");

            RuleFor(x => x.Team2Id)
                .MustAsync(async (team2Id, cancellation) => await _teamRepository.GetByIdAsync(team2Id) != null)
                .WithMessage("Team 2 does not exist.")
                .When(x => !string.IsNullOrWhiteSpace(x.Team2Id));

            RuleFor(x => x)
                .Must(x => x.Team1Id != x.Team2Id)
                .WithMessage("Team 1 and Team 2 cannot be the same.")
                .When(x => !string.IsNullOrWhiteSpace(x.Team1Id) && !string.IsNullOrWhiteSpace(x.Team2Id));

            RuleFor(x => x.WinningTeamId)
                .MaximumLength(100).WithMessage("Winning Team ID must not exceed 100 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.WinningTeamId));

            RuleFor(x => x.WinningTeamId)
                .MustAsync(async (winningTeamId, cancellation) => winningTeamId == null || 
                    await _teamRepository.GetByIdAsync(winningTeamId) != null)
                .WithMessage("Winning Team does not exist.")
                .When(x => !string.IsNullOrWhiteSpace(x.WinningTeamId));

            RuleFor(x => x)
                .Must(x => string.IsNullOrWhiteSpace(x.WinningTeamId) ||
                    x.WinningTeamId == x.Team1Id ||
                    x.WinningTeamId == x.Team2Id)
                .WithMessage("Winning Team must be one of the teams playing in the match.");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than 0.");    
        }
    }
}
