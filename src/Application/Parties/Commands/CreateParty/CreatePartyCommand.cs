using MediatR;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Commands.CreateParty;

public sealed class CreatePartyCommand : IRequest<PartyEntity>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}