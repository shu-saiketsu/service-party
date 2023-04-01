using MediatR;

namespace Saiketsu.Service.Party.Domain.IntegrationEvents;

public sealed class PartyDeletedIntegrationEvent : IRequest
{
    public int Id { get; set; }
}