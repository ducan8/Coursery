
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.IServices
{
    public interface IPhotoService
    {
        Task<string> AddPhotoAsync(IFormFile photo);
    }
}
