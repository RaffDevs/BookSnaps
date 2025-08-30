namespace BookSnaps.Domain.Entities;

public class Highlight
{
    public int Id { get; init; }
    public string Text { get; init; } = string.Empty;
    public int Page { get; init; } 
    public int BookId { get; init; }
    public Book Book { get; init; }
}