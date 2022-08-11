using FirstAttempt.Core.Model;
using FirstAttempt.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstAttempt.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {

            //Single or default birden fazla bulursa hata verir
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();
        }
    }
}
