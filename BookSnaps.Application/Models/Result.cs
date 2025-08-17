using BookSnaps.Domain.Models;

namespace BookSnaps.Application.Models;

public class Result<T>
{
    public T? Data { get; set; } 
    public BookSnapsError? Error { get; set; }
    public bool IsSuccess => Error is null;


    protected Result(T? value, BookSnapsError error) 
    {
        Data = value;
        Error = error; 
    }


    public static Result<T> Success(T value)
    {
        return new Result<T>(value, null);
    }

    public static Result<T> Failure(BookSnapsError error)
    {
        return new Result<T>(default, error);
    }
}