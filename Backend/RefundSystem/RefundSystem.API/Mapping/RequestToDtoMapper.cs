using AutoMapper;
using RefundSystem.API.Requests;
using RefundSystem.API.Requests.Auth;
using RefundSystem.Application.Dtos;

namespace RefundSystem.API.Mapping
{
    public class RequestToDtoMapper : Profile
    {
        public RequestToDtoMapper()
        {
            CreateMap<CreateCategoryRequest, CreateCategoryDto>();
            CreateMap<UpdateCategoryRequest, UpdateCategoryDto>();
            CreateMap<RegisterAuthRequest, RegisterAuthDto>();
        }
    }
}
