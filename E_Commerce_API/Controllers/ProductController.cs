using E_Commerce_Business.Repository.IRepository;
using E_Commerce_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _productRepository.GetAll());
        }

        [HttpGet("getBook/{productId}")]
        public async Task<IActionResult> Get(int? productId)
        {
            if (productId == null || productId == 0)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound

                });
            }
            var product = await _productRepository.GetById(productId.Value);
            if (product == null)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound,
                });
            }
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = await _productRepository.GetProductByCategoryId(id);
            if (result.Count == 0)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Product is not found",
                    StatusCode = StatusCodes.Status404NotFound,


                });
            }
            return Ok(result);
        }
    }
}