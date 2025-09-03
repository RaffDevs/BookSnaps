using BookSnaps.Application.Features.Books.Models.InputModels;
using BookSnaps.Application.Models;
using BookSnaps.Domain.Entities;
using BookSnaps.Domain.Models;
using BookSnaps.Infra.Persistence.Context;
using Cortex.Mediator;
using Cortex.Mediator.Commands;

namespace BookSnaps.Application.Features.Books.Commands.Create;

public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, Result<Unit>>
{
    private readonly BookSnapsDbContext _context;

    public CreateBookCommandHandler(BookSnapsDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Unit>> Handle(CreateBookCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var book = new Book
            {
                Title = command.Model.Title,
                Description = command.Model.Description,
                Authors = command.Model.Author,
                Publisher = command.Model.Publisher,
                Isbn = command.Model.Isbn,
                OwnerId = command.OwnerId
            };
            
           await _context.Books.AddAsync(book, cancellationToken); 
           await _context.SaveChangesAsync(cancellationToken);
           return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
           var error = new BookSnapsError(typeof(CreateBookCommandHandler), "Erro ao adicionar livro!", ex);
           error.ShowException();
           return Result<Unit>.Failure(error);
        }
    }
}