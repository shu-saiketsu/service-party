using FluentValidation;
using MediatR;
using Saiketsu.Service.Party.Application.Common;
using Saiketsu.Service.Party.Domain.Entities;
using Saiketsu.Service.Party.Domain.IntegrationEvents;

namespace Saiketsu.Service.Party.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommandHandler : IRequestHandler<CreatePartyCommand, PartyEntity>
{
    private readonly IApplicationDbContext _context;
    private readonly IEventBus _eventBus;
    private readonly IValidator<CreatePartyCommand> _validator;

    public CreatePartyCommandHandler(IApplicationDbContext context, IValidator<CreatePartyCommand> validator,
        IEventBus eventBus)
    {
        _context = context;
        _validator = validator;
        _eventBus = eventBus;
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

        var @event = new PartyCreatedIntegrationEvent
        {
            Id = party.Id,
            Name = party.Name,
            Description = party.Description
        };

        _eventBus.Publish(@event);

        return party;
    }
}