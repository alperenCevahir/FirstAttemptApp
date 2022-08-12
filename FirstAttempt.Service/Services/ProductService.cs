using AutoMapper;
using FirstAttempt.Core.DTOs;
using FirstAttempt.Core.Model;
using FirstAttempt.Core.Repositories;
using FirstAttempt.Core.Services;
using FirstAttempt.Core.UnitOfWorks;

namespace FirstAttempt.Service.Services
{
    public class ProductService : Service<Product>, IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository)
            : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var product = await _productRepository.GetProductsWithCategory();

            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(product);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
    }
}

