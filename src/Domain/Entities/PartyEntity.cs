namespace Saiketsu.Service.Party.Domain.Entities;

public sealed class PartyEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}