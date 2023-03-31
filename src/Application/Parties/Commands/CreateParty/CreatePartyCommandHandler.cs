using FluentValidation;
using MediatR;
using Saiketsu.Service.Party.Application.Common;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandHandler : IRequestHandler<CreatePartyCommand, PartyEntity>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<CreatePartyCommand> _validator;

    public CreatePartyCommandHandler(IApplicationDbContext context, IValidator<CreatePartyCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<PartyEntity> Handle(CreatePartyCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var party = new PartyEntity
        {
            Name = request.Name,
            Description = request.Description
        };

        await _context.Parties.AddAsync(party, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return party;
    }
}