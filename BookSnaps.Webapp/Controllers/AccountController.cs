using System.Security.Claims;
using BookSnaps.Application.Features.Account.Models;
using BookSnaps.Application.Features.Owner.Commands.Create;
using BookSnaps.Application.Models;
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
            TempData["ToastMessage"] = "Oops... some fields are invalid!";
            TempData["ToastType"] = ToastType.Error.Humanize();
            TempData["ToastLabel"] = "Error";

            return View(model);
        }

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

        if (!result.Succeeded)
        {
            TempData["ToastMessage"] = "Invalid credentials";
            TempData["ToastType"] = ToastType.Error.Humanize();
            TempData["ToastLabel"] = "Error";

            return View(model);
        }

        TempData["ToastLabel"] = "Success";
        TempData["ToastMessage"] = "Welcome to Booksnaps!";
        TempData["ToastType"] = ToastType.Success.Humanize();
        return RedirectToAction("Index", "Home");
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
            TempData["ToastMessage"] = "Oops... some fields are invalid!";
            TempData["ToastType"] = ToastType.Error.Humanize();
            TempData["ToastLabel"] = "Error";
            return View(model);
        }

        if (model.Password != model.ConfirmPassword)
        {
            ModelState.AddModelError(string.Empty, "Passwords do not match!");
            ;
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

            TempData["ToastMessage"] = $"Oops... Register failed!";
            TempData["ToastType"] = ToastType.Error.Humanize();
            TempData["ToastLabel"] = "Error";

            return View(model);
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, $"{model.FirstName} {model.SurName}")
        };

        var claimsResult = await _userManager.AddClaimsAsync(user, claims);

        if (!claimsResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);
            TempData["ToastMessage"] = $"Oops... Register failed!";
            TempData["ToastType"] = ToastType.Error.Humanize();
            TempData["ToastLabel"] = "Error";
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
            TempData["ToastMessage"] = $"Erro ao criar usuário!";
            TempData["ToastType"] = ToastType.Error.Humanize();
            TempData["ToastLabel"] = "Error";
            return View(model);
        }

        await _signInManager.SignInAsync(user, false);
        TempData["ToastLabel"] = "Success";
        TempData["ToastMessage"] = "Welcome to Booksnaps";
        TempData["ToastType"] = ToastType.Success.Humanize();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        TempData["ToastLabel"] = "Success";
        TempData["ToastMessage"] = "See you soon!";
        TempData["ToastType"] = ToastType.Success.Humanize();

        return RedirectToAction("Login", "Account");
    }
}