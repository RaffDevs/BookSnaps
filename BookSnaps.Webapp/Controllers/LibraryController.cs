using System.Security.Claims;
using BookSnaps.Application.Features.Books.Commands.Create;
using BookSnaps.Application.Features.Books.Models.InputModels;
using BookSnaps.Application.Features.Books.Queries.CountBooks;
using BookSnaps.Application.Models;
using BookSnaps.Webapp.Extensions;
using BookSnaps.Webapp.Models.Enums;
using Cortex.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookSnaps.Webapp.Controllers;

[Authorize]
public class LibraryController : Controller
{
    private readonly IMediator _mediator;

    public LibraryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _mediator
            .SendQueryAsync<CountBooksQuery, Result<int>>(new CountBooksQuery { OwnerId = userId });

        if (!result.IsSuccess)
        {
            this.AddToastMessage(result.Error.ToString(), ToastType.Error, "Error");
        }
        
        return View(result.Data);
    }
    
    [HttpGet]
    public async Task<IActionResult> Grid(string search)
    {
        return ViewComponent("BookList", new {search});
    }

    [HttpGet]
    public IActionResult AddBook()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddBook(CreateBookInputModel model)
    {
        if (!ModelState.IsValid)
        {
            this.AddToastMessage("Oops... alguns campos estão inválidos", ToastType.Error, "Error");
            return View(model);
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var command = new CreateBookCommand
        {
            OwnerId = userId,
            Model = model
        };
        
        var result = await _mediator.SendCommandAsync<CreateBookCommand, Result<Unit>>(command);

        if (!result.IsSuccess)
        {
            this.AddToastMessage(result.Error.ToString(), ToastType.Error, "Error");
            return View(model);
        }
        
        return RedirectToAction("Index", "Library");
    }
}