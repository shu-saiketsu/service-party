using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Party.Application.Common;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Queries.GetParties;

public sealed class GetPartiesQueryHandler : IRequestHandler<GetPartiesQuery, List<PartyEntity>>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<GetPartiesQuery> _validator;

    public GetPartiesQueryHandler(IValidator<GetPartiesQuery> validator, IApplicationDbContext context)
    {
        _validator = validator;
        _context = context;
    }

    public async Task<List<PartyEntity>> Handle(GetPartiesQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var party = await _context.Parties
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return party;
    }
}