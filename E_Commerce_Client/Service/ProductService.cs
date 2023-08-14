using E_Commerce_Client.Service.IService;
using E_Commerce_Models;
using Newtonsoft.Json;
using System;

namespace E_Commerce_Client.Service
{
    public class ProductService : IProductService
    {
        private  HttpClient _httpClient;
        //private IConfiguration _configuration;
        //private string BaseServerUrl;
        public ProductService(HttpClient httpClient/*, IConfiguration configuration*/)
        {
            _httpClient = httpClient;
            //_configuration = configuration;
            //BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            var response = await _httpClient.GetAsync("/api/product/getall");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                return products;
            }
            return new List<ProductDTO>();
        }

        //public async Task<IEnumerable<ProductDTO>> GetAll()
        //{
        //    var result = await _httpClient.GetAsync("/api/product/getall");
        //    if (result.IsSuccessStatusCode)
        //    {
        //        var content = await result.Content.ReadAsStringAsync();
        //        var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
        //        Console.Write(products);
        //        foreach (var item in products)
        //        {
        //            item.ImageUrl = BaseServerUrl + item.ImageUrl;
        //        }
        //        return products;
        //    }
        //    return new List<ProductDTO>();
        //}

        public async Task<ProductDTO> Get(int productId)
        {
            var result = await _httpClient.GetAsync($"/api/product/getBook/{productId}");
            var content = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                //product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorResponseDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<List<ProductDTO>> GetProductByCategoryId(int categoryId)
        {
            var result = await _httpClient.GetAsync($"/api/product/{categoryId}");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductDTO>>(content);
                //foreach (var item in products)
                //{
                //    item.ImageUrl = BaseServerUrl + item.ImageUrl;
                //}
                return products;
            }
            return new List<ProductDTO>();
        }
    }
}