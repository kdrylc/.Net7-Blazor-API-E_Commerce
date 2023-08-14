using E_Commerce_Client.Service.IService;
using E_Commerce_Models;
using Newtonsoft.Json;

namespace E_Commerce_Client.Service
{
    public class CategoryService : ICategoryService
    {
        private HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/category/getall");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDTO>>(content);
                return categories;
            }
            return new List<CategoryDTO>();
        }
    }
}
