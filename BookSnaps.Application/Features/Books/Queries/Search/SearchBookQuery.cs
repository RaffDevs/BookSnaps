using BookSnaps.Application.Features.Books.Models.InputModels;
using BookSnaps.Application.Models;
using Cortex.Mediator.Queries;

namespace BookSnaps.Application.Features.Books.Queries.Search;

public class SearchBookQuery : IQuery<Result<CreateBookInputModel>>
{
   public string Isbn { get; set; } = string.Empty;
}