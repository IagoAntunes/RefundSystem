using Microsoft.AspNetCore.Http;
using RefundSystem.Domain.Dtos;

namespace RefundSystem.Application.Services.Interfaces
{
    public interface IImageService
    {
        Task<ImageDto?> UploadImageAsync(IFormFile file, Guid id, Guid userId, string name);
        Task<ImageDto?> GetImage(Guid image);

    }
}
