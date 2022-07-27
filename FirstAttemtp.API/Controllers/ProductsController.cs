using AutoMapper;
using FirstAttempt.Core.DTOs;
using FirstAttempt.Core.Model;
using FirstAttempt.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstAttemtp.API.Controllers
{
    //mümkün olduğunca temiz kullanıcaz business kodları burada bulundurmayacağız o yüzden service katmanımız var
    //mapleme olayını burda gerçekleştiricez
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        public ProductsController(IMapper mapper, IService<Product> service)
        {
            _mapper = mapper;
            _service = service;
        }

        public async Task<IActionResult> All()
        {
            var products = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDto>>(products.ToList());
            //return Ok(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));

            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }
    }
}
