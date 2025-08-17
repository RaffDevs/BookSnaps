using BookSnaps.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSnaps.Infra.Persistence.Configuration;

public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
{
    public void Configure(EntityTypeBuilder<Owner> builder)
    {
        builder.ToTable("Owners");
        builder.HasKey(x => x.Id);
        builder
            .HasIndex(o => o.Email).IsUnique();
        builder.HasOne<IdentityUser>()
            .WithOne()
            .HasForeignKey<Owner>(x => x.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}