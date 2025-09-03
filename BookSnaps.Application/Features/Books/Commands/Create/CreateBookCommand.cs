using BookSnaps.Application.Features.Books.Models.InputModels;
using BookSnaps.Application.Models;
using Cortex.Mediator;
using Cortex.Mediator.Commands;

namespace BookSnaps.Application.Features.Books.Commands.Create;

public class CreateBookCommand : ICommand<Result<Unit>>
{
    public string OwnerId { get; set; }
    public CreateBookInputModel Model { get; set; }
}