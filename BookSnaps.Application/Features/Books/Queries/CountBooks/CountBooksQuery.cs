using BookSnaps.Application.Models;
using Cortex.Mediator.Queries;

namespace BookSnaps.Application.Features.Books.Queries.CountBooks;

public class CountBooksQuery : IQuery<Result<int>>
{
   public string OwnerId { get; set; }
}