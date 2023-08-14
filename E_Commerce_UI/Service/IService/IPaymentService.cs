using E_Commerce_Models;

namespace E_Commerce_UI.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessResponseDTO> Checkout(StripePaymentDTO model);
    }
}
