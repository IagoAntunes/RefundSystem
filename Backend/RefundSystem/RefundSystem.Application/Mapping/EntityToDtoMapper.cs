using AutoMapper;
using RefundSystem.Application.Dtos;
using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Application.Mapping
{
    public class EntityToDtoMapper : Profile
    {
        public EntityToDtoMapper()
        {
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CreateCategoryDto, CategoryEntity>();
            CreateMap<UpdateCategoryDto, CategoryEntity>();

            //Refund
            CreateMap<CreateRefundDto, RefundEntity>();
            CreateMap<RefundEntity, RefundDto>();

            //Image
            CreateMap<ImageDto, ImageEntity>().ReverseMap();
        }
    }
}
