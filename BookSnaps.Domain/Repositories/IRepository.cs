namespace BookSnaps.Domain.Repositories;

public interface IRepository
{
    public Task SaveChangesAsync();
}