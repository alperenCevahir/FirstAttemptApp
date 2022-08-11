using FirstAttempt.Core.DTOs;
using FirstAttempt.Core.Model;

namespace FirstAttempt.Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        public Task<CustomResponseDto<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
    }
}
