using BookSnaps.Domain.Entities;
using BookSnaps.Domain.Repositories;
using BookSnaps.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookSnaps.Infra.Persistence.Repositories;

public class HighlightRepository : IRepository
{
    private readonly BookSnapsDbContext _context;

    public HighlightRepository(BookSnapsDbContext context)
    {
        _context = context;
    }

    public DbSet<Highlight> Highlights => _context.Highlights;
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();   
    }
}