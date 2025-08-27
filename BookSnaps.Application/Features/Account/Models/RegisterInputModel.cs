using System.ComponentModel.DataAnnotations;

namespace BookSnaps.Application.Features.Account.Models;

public class RegisterInputModel
{
    [Required(ErrorMessage = "Nome precisa ser preenchido!")]
    [Display(Name = "Nome")]
    public string FirstName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Sobrenome precisa ser preenchido!")]
    [Display(Name = "Sobrenome")]
    public string SurName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Email precisa ser preenchido!")]
    [EmailAddress(ErrorMessage = "Email inválido!")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Senha precisa ser preenchida!")]
    [Display(Name = "Senha")]
    public string Password { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Confirme sua senha!")]
    [Display(Name = "Confirma senha")]
    [Compare("Password", ErrorMessage = "Senhas não conferem!")]
    public string ConfirmPassword { get; set; } = string.Empty;
}