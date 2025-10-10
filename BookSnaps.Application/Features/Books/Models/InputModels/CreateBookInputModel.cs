using System.ComponentModel.DataAnnotations;

namespace BookSnaps.Application.Features.Books.Models.InputModels;

public class CreateBookInputModel
{
    [Required(ErrorMessage = "Titulo deve ser preenchido!")]
    [Length(minimumLength: 1, maximumLength: 200, ErrorMessage = "Titulo deve ter entre 1 e 200 caracteres!")]
    [Display(Name = "Título")]
    public string? Title { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Descrição deve ser preenchida!")]
    [Length(minimumLength: 1, maximumLength: 5000, ErrorMessage = "Descrição deve ter entre 1 e 5000 caracteres!")]
    [Display(Name = "Descrição")]
    public string? Description { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Autor deve ser preenchido!")]
    [Length(minimumLength: 1, maximumLength: 200, ErrorMessage = "Deve ter entre 1 e 200 caracteres!")]
    [Display(Name = "Autor")]
    public string? Author { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Editora deve ser preenchida!")]
    [Length(minimumLength: 1, maximumLength: 200, ErrorMessage = "Deve ter entre 1 e 200 caracteres!")]
    [Display(Name = "Editora")]
    public string? Publisher { get; init; } = string.Empty;
    
    [Required(ErrorMessage = "Numero de paginas deve ser preenchido!")]
    [Range(1, int.MaxValue, ErrorMessage = "Deve ser um numero maior que 0!")]
    [Display(Name = "Paginas")]
    public int? PageCount { get; init; }
    
    [Required(ErrorMessage = "Isbn deve ser preenchido!")]
    [Length(minimumLength: 10, maximumLength: 13, ErrorMessage = "Deve ter entre {0} e {1} caracteres!")]
    public string Isbn { get; init; } = string.Empty;
}