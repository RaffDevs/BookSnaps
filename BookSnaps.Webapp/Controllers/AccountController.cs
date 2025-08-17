using BookSnaps.Application.Features.Account.Models;
using Cortex.Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSnaps.Webapp.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMediator _mediator;
    
    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mediator = mediator;
    }
    
    [HttpGet] 
    public IActionResult Register() 
    {
        return View();
    }

    [HttpPost]
    public Task<IActionResult> Register(RegisterInputModel model)
    {
        if (!ModelState.IsValid)
        {
            
        }
        
    } 
}