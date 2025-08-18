using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using RefundSystem.Application.Services.Interfaces;
using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using RefundSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Application.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository repository;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment environment;

        public ImageService(
            IImageRepository repository,
            IMapper mapper,
            IWebHostEnvironment environment
            )
        {
            this.repository = repository;
            this.mapper = mapper;
            this.environment = environment;
        }

        public async Task<ImageDto?> GetImage(Guid image)
        {
            var imageEntity = await repository.GetImage(image);
            if(imageEntity == null)
            {
                return null;
            }
            var imageDto = mapper.Map<ImageDto>(imageEntity);
            return imageDto;
        }

        public async Task<ImageDto?> UploadImageAsync(IFormFile file, Guid id, Guid userId, string name)
        {
            var filePath = await SaveFileToDiskAsync(file);
            if (filePath == null)
            {
                return null;
            }

            var imageDto = new ImageDto
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Name = name,
                FilePath = filePath,
                ContentType = file.ContentType,
            };
            var imageEntity = await repository.UploadImageAsync(imageDto);
            if(imageEntity == null)
            {
                return null;
            }

            var createdImageDto = mapper.Map<ImageDto>(imageEntity);
            return createdImageDto;
        }

        /// <summary>
        /// Trabalhador: Responsável APENAS por salvar o arquivo no disco e retornar seus metadados.
        /// </summary>
        private async Task<string?> SaveFileToDiskAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(environment.ContentRootPath, "Images");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // O NOME DO ARQUIVO AGORA É SÓ ISSO! Simples, limpo e único.
            var fileExtension = Path.GetExtension(file.FileName);
            var storedFileName = $"{Guid.NewGuid()}{fileExtension}";

            var filePath = Path.Combine(uploadsFolder, storedFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("Images", storedFileName);
        }
    }
}
