using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Domain.Repositories
{
    public interface IImageRepository
    {
        Task<ImageEntity?> UploadImageAsync(ImageDto imageDto);
        Task<ImageEntity?> GetImage(Guid imageId);
    }
}
