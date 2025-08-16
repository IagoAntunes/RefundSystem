using RefundSystem.Application.Dtos;
using RefundSystem.Domain.Dtos;

namespace RefundSystem.Application.Services
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllCategories();
        Task<CategoryDto> CreateCategory(CreateCategoryDto createCategory);
        Task<CategoryDto?> UpdateCategory(Guid categoryId, UpdateCategoryDto updateCategory);
        Task<CategoryDto?> DeleteCategory(Guid categoryId);

    }
}
