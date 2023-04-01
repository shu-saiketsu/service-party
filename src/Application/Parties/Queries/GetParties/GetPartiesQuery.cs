using MediatR;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Parties.Queries.GetParties;

public sealed class GetPartiesQuery : IRequest<List<PartyEntity>>
{
}