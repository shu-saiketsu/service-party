using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Party.Application.Common;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Queries.GetParty;

public sealed class GetPartyQueryHandler : IRequestHandler<GetPartyQuery, PartyEntity?>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetPartyQuery> _validator;

    public GetPartyQueryHandler(IApplicationDbContext context, IValidator<GetPartyQuery> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<PartyEntity?> Handle(GetPartyQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var party = await _context.Parties
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return party;
    }
}