using _5_days_in_the_clouds_2024.Domain.Contracts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5_days_in_the_clouds_2024.Application.Features.Player.Commands.CreatePlayer
{
    public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
    {
        private readonly IPlayerRepository _playerRepository;
        public CreatePlayerCommandValidator(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;

            RuleFor(x => x.Nickname)
                .NotEmpty().WithMessage("Nickname is required.")
                .MaximumLength(50).WithMessage("Nickname must not exceed 50 characters.");

            RuleFor(x => x.Nickname)
                .MustAsync(async (nickname, cancellation) => !await _playerRepository.PlayerExisist(nickname))
                .WithMessage("A player with this nickname already exists.")
                .When(x => !string.IsNullOrWhiteSpace(x.Nickname)); ;

        }
    }
}
