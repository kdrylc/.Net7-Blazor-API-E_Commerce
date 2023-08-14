using E_Commerce_Models;

namespace E_Commerce_UI.Service.IService
{
    public interface IDiscountService
    {
        public Task<ServiceResponse<DiscountDTO>> GetImplementCode(string code);
    }
}
