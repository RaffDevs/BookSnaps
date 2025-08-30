using BookSnaps.Domain.Entities;
using BookSnaps.Domain.Repositories;
using BookSnaps.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookSnaps.Infra.Persistence.Repositories;

public class BookRepository : IRepository
{
    private readonly BookSnapsDbContext _context;
    
    public BookRepository(BookSnapsDbContext context)
    {
        _context = context;
    }
    
   public DbSet<Book> Books => _context.Books;
   
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}