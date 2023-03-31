using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Saiketsu.Service.Party.Application.Common;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PartyEntity> Parties { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}