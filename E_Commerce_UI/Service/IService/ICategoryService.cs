using E_Commerce_Models;

namespace E_Commerce_UI.Service.IService
{
    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryDTO>> GetAll();
    }
}
