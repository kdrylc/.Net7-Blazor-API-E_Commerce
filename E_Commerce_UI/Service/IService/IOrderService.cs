using E_Commerce_Models;

namespace E_Commerce_UI.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> GetAll(string? userId);
        public Task<OrderDTO> Get(int orderId);
        public Task<OrderDTO> Create(StripePaymentDTO paymentDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccessfull(OrderHeaderDTO orderHeader);
    }
}
