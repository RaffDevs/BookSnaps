namespace BookSnaps.Domain.Entities;

public class Book
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string SubTitle { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public IEnumerable<string> Authors { get; init; } = [];
    public string Publisher { get; init; } = string.Empty;
    public int PageCount { get; init; }
    public string CoverUrl { get; init; } = string.Empty;
    public string Isbn { get; init; } = string.Empty;
    public string OwnerId { get; init; }
    public Owner Owner { get; init; }
}