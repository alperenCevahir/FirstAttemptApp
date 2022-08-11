using FirstAttempt.Core.DTOs;
using FirstAttempt.Core.Model;

namespace FirstAttempt.Core.Services
{
    public interface IProductService : IService<Product>
    {

        Task<List<ProductWithCategoryDto>> GetProductsWithCategory();

    }
}
