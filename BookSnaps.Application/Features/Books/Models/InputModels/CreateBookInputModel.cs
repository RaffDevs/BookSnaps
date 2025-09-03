using System.ComponentModel.DataAnnotations;

namespace BookSnaps.Application.Features.Books.Models.InputModels;

public class CreateBookInputModel
{
    [Required(ErrorMessage = "Titulo deve ser preenchido!")]
    [Display(Name = "Título")]
    public string Title { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Descrição deve ser preenchida!")]
    [Display(Name = "Descrição")]
    public string Description { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Autor deve ser preenchido!")]
    [Display(Name = "Autor")]
    public string Author { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Editora deve ser preenchida!")]
    [Display(Name = "Editora")]
    public string Publisher { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Numero de paginas deve ser preenchido!")]
    [Display(Name = "Paginas")]
    public int PageCount { get; set; }
    
    [Required(ErrorMessage = "Isbn deve ser preenchido!")]
    public string Isbn { get; set; } = string.Empty;
}