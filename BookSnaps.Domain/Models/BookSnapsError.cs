namespace BookSnaps.Domain.Models;

public class BookSnapsError 
{
    private Type Origin { get; set; }
    private string Message { get; set; } = string.Empty;
    private Exception Exception { get; set; }
    
    public BookSnapsError(Type origin, string message, Exception exception)
    {
        Origin = origin;
        Message = message;
        Exception = exception;
    }

    public override string ToString()
    {
        return $"{Message}";
    }

    public void ShowException()
    {
        Console.WriteLine($"{Origin.Name} - {Exception.Message}");
    }
}