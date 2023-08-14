using E_Commerce_Business.Repository.IRepository;
using E_Commerce_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = await _categoryRepository.GetAll();
            if (response == null)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Categories are not found",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(response);
        }
    }
}