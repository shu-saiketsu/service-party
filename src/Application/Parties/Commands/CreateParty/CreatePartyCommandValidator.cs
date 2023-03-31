using FluentValidation;

namespace Saiketsu.Service.Party.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandValidator : AbstractValidator<CreatePartyCommand>
{
    public CreatePartyCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}