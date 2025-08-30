using BookSnaps.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookSnaps.Infra.Persistence.Configuration;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasIndex(b => b.Id);
        builder
            .HasOne(b => b.Owner)
            .WithMany(o => o.Books)
            .HasForeignKey(b => b.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasMany(b => b.Highlights)
            .WithOne(h => h.Book)
            .HasForeignKey(h => h.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}