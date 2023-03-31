using FluentValidation;
using MediatR;
using Saiketsu.Service.Party.Application.Common;

namespace Saiketsu.Service.Party.Application.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommandHandler : IRequestHandler<DeletePartyCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IValidator<DeletePartyCommand> _validator;

    public DeletePartyCommandHandler(IApplicationDbContext context, IValidator<DeletePartyCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<bool> Handle(DeletePartyCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var party = await _context.Parties.FindAsync(request.Id, cancellationToken);
        if (party == null)
            return false;

        _context.Parties.Remove(party);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}