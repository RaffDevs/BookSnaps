using BookSnaps.Application.Features.Books.Models.ViewModels;
using BookSnaps.Application.Models;
using Cortex.Mediator.Queries;

namespace BookSnaps.Application.Features.Books.Queries.GetAll;

public class GetAllBooksQuery : IQuery<Result<List<BookViewModel>>>
{
   public string OwnerId { get; set; }
   public string Search { get; set; } = string.Empty;
}