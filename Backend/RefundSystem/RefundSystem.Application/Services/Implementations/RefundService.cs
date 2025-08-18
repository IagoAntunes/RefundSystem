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
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public RefundService(
            IRefundRepository refundRepository,
            IMapper mapper,
            IWebHostEnvironment environment,
            IImageService imageService
            )
        {
            this.refundRepository = refundRepository;
            this.mapper = mapper;
            this.environment = environment;
            this.imageService = imageService;
        }

        public IWebHostEnvironment Environment { get; }

        public async Task<RefundDto> CreateRefund(CreateRefundDto createRefund, Guid userId)
        {
            var image = await imageService.UploadImageAsync(createRefund.File, Guid.NewGuid(), userId, createRefund.File.FileName);

            var refundEntity = mapper.Map<RefundEntity>(createRefund);

            refundEntity.ImageId = image.Id;
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



    }
}
