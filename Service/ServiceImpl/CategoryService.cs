using CRUD.Models;
using CRUD.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Service.ServiceImpl
{
    public class CategoryService : ICategoryService
    {
        private readonly CRUDDBContext context;

        public CategoryService(CRUDDBContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoryAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FindAsync(id);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await context.SaveChangesAsync();
            return category;
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await context.Categories.FindAsync(id);
            if (existingCategory == null)
            {
                return false;
            }

            existingCategory.Categoryname = category.Categoryname;
            

            context.Categories.Update(existingCategory);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
            {
                return false;
            }

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return true;
        }

    }
}
