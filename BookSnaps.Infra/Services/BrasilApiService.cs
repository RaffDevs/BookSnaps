using BookSnaps.Domain.Entities;
using BookSnaps.Infra.Services.DTOs;
using Refit;

namespace BookSnaps.Infra.Services;

public interface IBrasilApiService
{
   [Get("/api/isbn/v1/{isbn}")] 
   Task<BrasilApiResult> GetBookByIsbnAsync(string isbn);
}