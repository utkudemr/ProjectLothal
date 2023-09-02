

using Business.Features.Starships.Commands.Create;
using FluentValidation;

namespace Application.Features.Starships.Commands.Create;

public class CreateStarshipValidator:AbstractValidator<CreateStarshipCommand>
{
    public CreateStarshipValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MinimumLength(2);
    }
}
