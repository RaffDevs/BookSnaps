using BookSnaps.Application.Constants;
using BookSnaps.Application.Features.Books.Models.InputModels;
using BookSnaps.Application.Models;
using BookSnaps.Domain.Models;
using BookSnaps.Infra.Services;
using Cortex.Mediator.Queries;

namespace BookSnaps.Application.Features.Books.Queries.Search;

public class SearchBookQueryHandler : IQueryHandler<SearchBookQuery, Result<CreateBookInputModel>>
{
    private readonly IBrasilApiService _brasilApiService;

    public SearchBookQueryHandler(IBrasilApiService brasilApiService)
    {
        _brasilApiService = brasilApiService;
    }

    public async Task<Result<CreateBookInputModel>> Handle(SearchBookQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _brasilApiService.GetBookByIsbnAsync(query.Isbn);
            var model = new CreateBookInputModel
            {
                Title = string.IsNullOrWhiteSpace(result.Title) ? Messages.NOT_INFORMED : result.Title,
                Description = string.IsNullOrEmpty(result.Synopsis) ? Messages.NOT_INFORMED : result.Synopsis,
                Author = result.Authors.Any() ? string.Join(", ", result.Authors) : Messages.NOT_INFORMED,
                Publisher = string.IsNullOrEmpty(result.Publisher) ? Messages.NOT_INFORMED : result.Publisher,
                Isbn = string.IsNullOrEmpty(result.Isbn) ? Messages.NOT_INFORMED : result.Isbn,
                PageCount = result.PageCount ?? 0
            };
            return Result<CreateBookInputModel>.Success(model);
        }
        catch (Exception ex)
        {
            var error = new BookSnapsError(typeof(SearchBookQueryHandler), "Erro ao pesquisar livros!", ex);
            error.ShowException();
            return Result<CreateBookInputModel>.Failure(error);
        }
    }
}