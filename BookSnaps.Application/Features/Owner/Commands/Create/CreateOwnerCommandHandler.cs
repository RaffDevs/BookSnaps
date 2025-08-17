using BookSnaps.Application.Models;
using BookSnaps.Infra.Persistence.Repositories;
using BookSnaps.Domain.Entities;
using BookSnaps.Domain.Models;
using Cortex.Mediator;
using Cortex.Mediator.Commands;
using OwnerEntity = BookSnaps.Domain.Entities.Owner;
namespace BookSnaps.Application.Features.Owner.Commands.Create;

public class CreateOwnerCommandHandler : ICommandHandler<CreateOwnerCommand, Result<Unit>>
{
    private readonly OwnerRepository _repository;
    
    public CreateOwnerCommandHandler(OwnerRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<Unit>> Handle(CreateOwnerCommand command, CancellationToken cancellationToken)
    {
        try
        {

            var owner = new OwnerEntity()
            {
                Id = command.Id,
                FirstName = command.FirstName,
                SurName = command.SurName,
                Email = command.Email
            };

            await _repository.BeginTransactionAsync();
            await _repository.Owners.AddAsync(owner, cancellationToken);
            await _repository.SaveChangesAsync();
            await _repository.CommitTransactionAsync();

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception exception)
        {
            var error = new BookSnapsError(typeof(CreateOwnerCommandHandler), "Erro ao criar owner!", exception);
            return Result<Unit>.Failure(error);
        }
    }
}