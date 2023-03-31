using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Application.Common;

public interface IApplicationDbContext
{
    DbSet<PartyEntity> Parties { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}