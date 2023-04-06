using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saiketsu.Service.Party.Domain.Entities;

namespace Saiketsu.Service.Party.Infrastructure.Persistence.Configurations;

internal class PartyEntityConfiguration : IEntityTypeConfiguration<PartyEntity>
{
    public void Configure(EntityTypeBuilder<PartyEntity> builder)
    {
        builder.ToTable("party");

        builder.Property(x => x.Id)
            .UseIdentityAlwaysColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(200);
    }
}