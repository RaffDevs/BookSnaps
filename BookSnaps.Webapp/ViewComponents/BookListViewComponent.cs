using System.Security.Claims;
using BookSnaps.Application.Features.Books.Models.ViewModels;
using BookSnaps.Application.Features.Books.Queries.GetAll;
using BookSnaps.Application.Models;
using BookSnaps.Webapp.Extensions;
using BookSnaps.Webapp.Models.Enums;
using Cortex.Mediator;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace BookSnaps.Webapp.ViewComponents;

public class BookListViewComponent : ViewComponent
{
    private readonly IMediator _mediator;

    public BookListViewComponent(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IViewComponentResult> InvokeAsync(string search = "")
    {
        var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new GetAllBooksQuery {OwnerId = userId, Search = search};
        var result = await _mediator.SendQueryAsync<GetAllBooksQuery, Result<List<BookViewModel>>>(query);
        
        if (result.IsSuccess)
        {
            return View(result.Data);
        }
        
        this.AddToastMessage(result.Error.ToString(), ToastType.Error, "Error");
        return View();
    }
}