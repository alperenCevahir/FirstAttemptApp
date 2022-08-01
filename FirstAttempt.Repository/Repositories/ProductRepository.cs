using FirstAttempt.Core.Model;
using FirstAttempt.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.Products.Include(x=> x.Category).ToListAsync();
        }
    }
}
