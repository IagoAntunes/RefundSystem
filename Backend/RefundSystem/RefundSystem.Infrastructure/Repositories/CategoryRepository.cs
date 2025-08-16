using Microsoft.EntityFrameworkCore;
using RefundSystem.Domain.Dtos;
using RefundSystem.Domain.Entities;
using RefundSystem.Domain.Repositories;
using RefundSystem.Infrastructure.Data;

namespace RefundSystem.Infrastructure.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly RefundSystemDbContext dbContext;

        public CategoryRepository(RefundSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CategoryEntity>> GetAllCategories()
        {
            var categories = await dbContext.Categories.ToListAsync();
            return categories;
        }

        public async Task<CategoryEntity> CreateCastegory(CategoryEntity category)
        {
            var result = await dbContext.Categories.AddAsync(category);
            await dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryEntity?> DeleteCategory(Guid categoryId)
        {
            var findedCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (findedCategory == null)
            {
                return null;
            }
            dbContext.Categories.Remove(findedCategory);
            await dbContext.SaveChangesAsync();
            return findedCategory;
        }

        public async Task<CategoryEntity?> UpdateCategory(Guid categoryId, CategoryEntity category)
        {
            var findedCategory = await dbContext.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (findedCategory == null)
            {
                return null;
            }
            findedCategory.Name = category.Name;
            dbContext.Categories.Update(findedCategory);
            await dbContext.SaveChangesAsync();
            return findedCategory;
        }
    }
}
