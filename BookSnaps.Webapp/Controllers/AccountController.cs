using System.Security.Claims;
using BookSnaps.Application.Features.Account.Models;
using BookSnaps.Application.Features.Owner.Commands.Create;
using BookSnaps.Application.Models;
using BookSnaps.Webapp.Extensions;
using BookSnaps.Webapp.Models.Enums;
using Cortex.Mediator;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookSnaps.Webapp.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IMediator _mediator;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
        IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginInputModel model)
    {
        if (!ModelState.IsValid)
        {
            this.AddToastMessage("Oops... alguns campos estão inválidos!", ToastType.Error, "Error");
            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (!result.Succeeded)
        {
            this.AddToastMessage("Credenciais inválidas", ToastType.Error, "Error");
            return View(model);
        }

        this.AddToastMessage("Bem vindo ao Booksnaps!", ToastType.Success, "Success");
        return RedirectToAction("Index", "Library");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterInputModel model)
    {
        if (!ModelState.IsValid)
        {
            this.AddToastMessage("Oops... alguns campos estão inválidos", ToastType.Error, "Error");
            return View(model);
        }

        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match!");
            return View(model);
        }

        var user = new IdentityUser
        {
            Email = model.Email,
            UserName = model.Email
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                if (error is { Code: "DuplicateUserName" or "DuplicateEmail" })
                {
                    ModelState.AddModelError("Email", "Email already registered!");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            this.AddToastMessage("Oops... Falha ao registrar!", ToastType.Error, "Error");;
            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.GivenName, $"{model.FirstName} {model.SurName}")
        };

        var claimsResult = await _userManager.AddClaimsAsync(user, claims);

        if (!claimsResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            this.AddToastMessage("Oops... Falha ao registrar!", ToastType.Error, "Error");
            return View(model);
        }
        
        var command = new CreateOwnerCommand
        {
            FirstName = model.FirstName,
            SurName = model.SurName,
            Email = model.Email,
            Id = user.Id
        };
        var owner = await _mediator.SendCommandAsync<CreateOwnerCommand, Result<Unit>>(command);
        
        if (!owner.IsSuccess)
        {
            await _userManager.DeleteAsync(user);
            this.AddToastMessage("Oops... Erro ao criar usuario!", ToastType.Error, "Error");
            return View(model);
        }

        await _signInManager.SignInAsync(user, false);
        this.AddToastMessage("Bem vindo ao Booksnaps!", ToastType.Success, "Success");;
        return RedirectToAction("Index", "Library");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        this.AddToastMessage("Até breve!", ToastType.Success, "Success");
        return RedirectToAction("Login", "Account");
    }
}