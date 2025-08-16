using AutoMapper;
using RefundSystem.Application.Dtos;
using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using RefundSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository repository;
        private readonly IMapper mapper;

        public CategoryService(
            ICategoryRepository repository,
            IMapper mapper
        )
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var result = await repository.GetAllCategories();
            var categoriesDto = mapper.Map<List<CategoryDto>>(result);
            return categoriesDto;
        }

        public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategory)
        {
            var categoryEntity = mapper.Map<CategoryEntity>(createCategory);
            var result = await repository.CreateCastegory(categoryEntity);
            var categoryDto = mapper.Map<CategoryDto>(result);

            return categoryDto;
        }

        public async Task<CategoryDto?> DeleteCategory(Guid categoryId)
        {
            var result = await repository.DeleteCategory(categoryId);
            if(result == null)
            {
                return null;
            }
            var categoryDto = mapper.Map<CategoryDto>(result);
            return categoryDto;
        }

        public async Task<CategoryDto?> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategory)
        {
            var categoryEntity = mapper.Map<CategoryEntity>(updateCategory);
            var result = await repository.UpdateCategory(categoryId, categoryEntity);
            if(result == null)
            {
                return null;
            }
            var categoryDto = mapper.Map<CategoryDto>(result);
            return categoryDto;
        }
    }
}
