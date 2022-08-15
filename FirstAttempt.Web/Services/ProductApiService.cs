using FirstAttempt.Core.DTOs;

namespace FirstAttempt.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategoryAsync()
        {
            //Api tarafından geliyor burası
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductWithCategoryDto>>>("products/GetProductsWithCategory");

            return response.Data;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"products/{id}");

            //if (response.Errors.Any())
            //{

            //}

            return response.Data;
            //Hata olma durumunda responsedan onu da return edebilirsin

        }

        public async Task<ProductDto> SaveAsync(ProductDto newProduct)
        {
            //Post etiketli tek bir şey olduğu için bunda linkin devamını verme gereği duymadık
            var response = await _httpClient.PostAsJsonAsync("products", newProduct);
            //Burda istersen loglamasını da yaparsın
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDto<ProductDto>>();
            //Yukardaki ifi geçtiyse 200 gelmiştir yani mutlaka datası dolu demek
            return responseBody.Data;
        }

        public async Task<bool> UpdateAsync(ProductDto newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("products", newProduct);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"products/{id}");
            return response.IsSuccessStatusCode;
        }




      
    }
}
