using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RefundSystem.Application.Dtos;
using RefundSystem.Application.Services.Interfaces;
using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using RefundSystem.Domain.Repositories;

namespace RefundSystem.Application.Services.Implementations
{
    public class RefundService : IRefundService
    {
        private readonly IRefundRepository refundRepository;
        private readonly IWebHostEnvironment environment;
        private readonly IMapper mapper;

        public RefundService(IRefundRepository refundRepository, IMapper mapper, IWebHostEnvironment environment)
        {
            this.refundRepository = refundRepository;
            this.mapper = mapper;
            this.environment = environment;
        }

        public IWebHostEnvironment Environment { get; }

        public async Task<RefundDto> CreateRefund(CreateRefundDto createRefund, Guid userId)
        {
            var filePath = await SaveFileAsync(createRefund.File,userId);

            var refundEntity = mapper.Map<RefundEntity>(createRefund);

            refundEntity.FilePath = filePath;
            refundEntity.UserId = userId;
            var result = await refundRepository.CreateRefund(refundEntity);
            var refundDto = mapper.Map<RefundDto>(result);
            return refundDto;
        }

        public async Task<RefundDto?> DeleteRefund(Guid userId, Guid refundId)
        {
            var result = await refundRepository.DeleteRefund(userId, refundId);
            if(result == null)
            {
                return null;
            }
            var refundDto = mapper.Map<RefundDto>(result);
            return refundDto;
        }

        public async Task<List<RefundDto>> GetRefundsByUser(Guid userId)
        {
            var result = await refundRepository.GetRefundsByUser(userId);
            var refundsDto = mapper.Map<List<RefundDto>>(result);
            return refundsDto;
        }

        private async Task<string> SaveFileAsync(IFormFile file, Guid userId)
        {
            if (file == null || file.Length == 0)
            {
                return null;
            }

            var uploadsFolder = Path.Combine(environment.ContentRootPath, "Images");

            var uniqueFileName = $"{userId}_{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Path.Combine("Images", uniqueFileName);
        }

    }
}
