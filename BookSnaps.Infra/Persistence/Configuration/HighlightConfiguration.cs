using BookSnaps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSnaps.Infra.Persistence.Configuration;

public class HighlightConfiguration : IEntityTypeConfiguration<Highlight>
{
    public void Configure(EntityTypeBuilder<Highlight> builder)
    {
        builder.ToTable("Highlights");
        builder.HasKey(x => x.Id);
        builder
            .HasOne(h => h.Book)
            .WithMany(b => b.Highlights)
            .HasForeignKey(h => h.BookId)
            .IsRequired();
    }
}