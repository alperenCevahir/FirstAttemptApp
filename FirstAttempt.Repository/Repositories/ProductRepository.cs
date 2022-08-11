using FirstAttempt.Core.Model;
using FirstAttempt.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FirstAttempt.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {

            //Lazy loading var bir de konunun dışında 

            //Eager loading
            //ilk productları çektiğimiz anda kategoriyi de çekersek eager loading oluyor
            return await _context.Products.Include(x => x.Category).ToListAsync();
        }
    }
}
