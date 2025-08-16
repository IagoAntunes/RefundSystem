using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefundSystem.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetAllCategories();
        Task<CategoryEntity> CreateCastegory(CategoryEntity category);
        Task<CategoryEntity?> UpdateCategory(Guid categoryId, CategoryEntity category);
        Task<CategoryEntity?> DeleteCategory(Guid categoryId);
    }
}
