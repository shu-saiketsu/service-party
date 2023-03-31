using MediatR;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Queries.GetParty;

public sealed class GetPartyQuery : IRequest<PartyEntity?>
{
    public int Id { get; set; }
}