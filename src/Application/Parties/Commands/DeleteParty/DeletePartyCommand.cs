using MediatR;

namespace Saiketsu.Service.Party.Application.Parties.Commands.DeleteParty;

public sealed class DeletePartyCommand : IRequest<bool>
{
    public int Id { get; set; }
}