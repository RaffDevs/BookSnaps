using BookSnaps.Application.Models;
using Cortex.Mediator;
using Cortex.Mediator.Commands;

namespace BookSnaps.Application.Features.Owner.Commands.Create;

public class CreateOwnerCommand : ICommand<Result<Unit>>
{
   public string Id { get; set; } 
   public string FirstName { get; set; } 
   public string SurName { get; set; } 
   public string Email { get; set; }
}