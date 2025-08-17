namespace BookSnaps.Domain.Entities;

public class Owner
{
   public string Id { get; init; }
   public string FirstName { get; init; } = string.Empty;
   public string SurName { get; init; } = string.Empty;
   public string Email { get; init; } = string.Empty;
   public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
   public IEnumerable<Book> Books { get; init; } = [];
}