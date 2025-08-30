using BookSnaps.Application.Features.Books.Models;
using BookSnaps.Application.Features.Books.Models.ViewModels;
using BookSnaps.Application.Models;
using BookSnaps.Domain.Models;
using BookSnaps.Infra.Persistence.Repositories;
using Cortex.Mediator.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookSnaps.Application.Features.Books.Queries.GetAll;

public class GetAllBooksQueryHandler : IQueryHandler<GetAllBooksQuery, Result<List<BookViewModel>>>
{
    private readonly BookRepository _bookRepository;
    
    public GetAllBooksQueryHandler(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public async Task<Result<List<BookViewModel>>> Handle(GetAllBooksQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var q = _bookRepository.Books
                .AsNoTracking()
                .Where(b => b.OwnerId == query.OwnerId);

            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                var s = query.Search.ToLower().Trim();
                q = q.Where(
                    b => 
                        b.Title.Contains(s.ToLower()) || 
                        b.Authors.Contains(s, StringComparison.CurrentCultureIgnoreCase));
            }
            
            var books = await q
                .OrderBy(b => b.Title)
                .Select(b => b.ToBookViewModel())
                .ToListAsync(cancellationToken);
            
            return Result<List<BookViewModel>>.Success(books);
        }
        catch (Exception ex)
        {
            var error = new BookSnapsError(typeof(GetAllBooksQueryHandler), "Erro ao buscar livros!", ex);
            error.ShowException();
            return Result<List<BookViewModel>>.Failure(error);
        }
    }
}