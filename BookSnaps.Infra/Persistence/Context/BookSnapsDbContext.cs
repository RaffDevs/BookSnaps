using System.Reflection;
using BookSnaps.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSnaps.Infra.Persistence.Context;

public class BookSnapsDbContext : IdentityDbContext<IdentityUser>
{
   public BookSnapsDbContext(DbContextOptions<BookSnapsDbContext> options) : base(options)
   {
   }
   
   public DbSet<Owner> Owners { get; set; }
   public DbSet<Book> Books { get; set; }

   protected override void OnModelCreating(ModelBuilder builder)
   {
      builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      base.OnModelCreating(builder);
   }
}