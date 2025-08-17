using BookSnaps.Domain.Entities;
using BookSnaps.Domain.Repositories;
using BookSnaps.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace BookSnaps.Infra.Persistence.Repositories;

public class OwnerRepository : IRepository
{
    private readonly BookSnapsDbContext _context;
    
    public OwnerRepository(BookSnapsDbContext context)
    {
        _context = context;
    }
    
    public DbSet<Owner> Owners => _context.Owners;
    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }
}