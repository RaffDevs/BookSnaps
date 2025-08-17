using System.ComponentModel.DataAnnotations;

namespace BookSnaps.Application.Features.Account.Models;

public class RegisterInputModel
{
    [Required(ErrorMessage = "First name is required!")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Surname is required!")]
    public string SurName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required!")]
    [EmailAddress(ErrorMessage = "Invalid email address!")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password is required!")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Confirm password is required!")]
    [Compare("Password", ErrorMessage = "Passwords do not match!")]
    public string ConfirmPassword { get; set; } = string.Empty;
}