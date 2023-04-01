using MediatR;

namespace Saiketsu.Service.Party.Domain.IntegrationEvents;

public sealed class PartyCreatedIntegrationEvent : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}