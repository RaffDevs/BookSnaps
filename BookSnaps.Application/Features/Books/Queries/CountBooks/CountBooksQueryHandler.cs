using BookSnaps.Application.Features.Books.Models.ViewModels;
using BookSnaps.Application.Features.Books.Queries.GetAll;
using BookSnaps.Application.Models;
using BookSnaps.Domain.Models;
using BookSnaps.Infra.Persistence.Repositories;
using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookSnaps.Application.Features.Books.Queries.CountBooks;

public class CountBooksQueryHandler : IQueryHandler<CountBooksQuery, Result<int>>
{
    private readonly BookRepository _bookRepository;
    
    public CountBooksQueryHandler(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<int>> Handle(CountBooksQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var count = await _bookRepository
                .Books
                .CountAsync(
                    b => b.OwnerId == query.OwnerId,
                    cancellationToken
                );

            return Result<int>.Success(count);
        }
        catch (Exception ex)
        {
            var error = new BookSnapsError(typeof(GetAllBooksQueryHandler), "Erro ao contar livros", ex);
            error.ShowException();
            return Result<int>.Failure(error);
        }
    }
}