using BookSnaps.Application.Features.Books.Models.ViewModels;
using BookSnaps.Domain.Entities;

namespace BookSnaps.Application.Features.Books.Models;

public static class BooksMapper
{
   public static BookViewModel ToBookViewModel(this Book book)
   {
       return new BookViewModel
       {
           Id = book.Id,
           Title = book.Title,
           Description = book.Description,
           Author = string.Join(", ", book.Authors),
           Publisher = book.Publisher,
           HighlightCount = book.Highlights.Count,
       };
   }
}