using AutoMapper;
using FirstAttempt.Core.DTOs;
using FirstAttempt.Core.Model;
using FirstAttempt.Core.Services;
using FirstAttempt.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

//Viewbag dropdown list ile alakalı sanırım
namespace FirstAttempt.Web.Controllers
{
    public class ProductsController : Controller
    {
        //private readonly IProductService _services;
        //private readonly ICategoryService _categoryService;
        //private readonly IMapper _mapper;

        //public ProductsController(IProductService services, ICategoryService categoryService, IMapper mapper)
        //{
        //    _services = services;
        //    _categoryService = categoryService;
        //    _mapper = mapper;
        //}

        //Hepsi APIden dtolu geldiği için kaldırdık

        private readonly ProductApiService _productApiService;
        private readonly CategoryApiService _categoryApiService;

        public ProductsController(CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        public async Task<IActionResult> Index()
        {
            //Katmanlı mimari sadece web uygulaması dönecekse custom response dönmeye gerek yok
            return View(await _productApiService.GetProductsWithCategoryAsync());
        }
        //Save
        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //Mappera gerek kalmadı direkt categories dto geliyo
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                await _productApiService.SaveAsync(productDto);

                //await _services.AddAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();

        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))] 
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);

            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                await _productApiService.UpdateAsync(productDto);
                //await _services.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index));
            }
            var categoriesDto = await _categoryApiService.GetAllAsync();
            //var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());


            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
