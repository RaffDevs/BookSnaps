using System.ComponentModel.DataAnnotations;

namespace BookSnaps.Application.Features.Account.Models;

public class LoginInputModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;
}