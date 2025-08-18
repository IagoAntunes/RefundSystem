using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using RefundSystem.Domain.Repositories;
using RefundSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly RefundSystemDbContext dbContext;

        public ImageRepository(RefundSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ImageEntity?> GetImage(Guid imageId)
        {
            var findedImage = await dbContext.Images.FindAsync(imageId);
            return findedImage;
        }

        public async Task<ImageEntity?> UploadImageAsync(ImageDto imageDto)
        {
            var imageEntity = new ImageEntity
            {
                Id = imageDto.Id,
                UserId = imageDto.UserId,
                Name = imageDto.Name,
                FilePath = imageDto.FilePath,
                ContentType = imageDto.ContentType,
            };

            var image = await dbContext.Images.AddAsync(imageEntity);
            await dbContext.SaveChangesAsync();

            if(image == null)
            {
                return null;
            }

            return imageEntity;
        }
    }
}
