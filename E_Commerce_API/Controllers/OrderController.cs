using E_Commerce_Business.Repository.IRepository;
using E_Commerce_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(int? orderHeaderId)
        {
            if (orderHeaderId == null || orderHeaderId == 0)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }

            var orderHeader = await _orderRepository.Get(orderHeaderId.Value);
            if (orderHeader == null)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status400BadRequest,
                });
            }
            return Ok(orderHeader);
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderRepository.GetAll();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {
            paymentDTO.Order.OrderHeader.OrderDate = DateTime.Now;
            var result = await _orderRepository.Create(paymentDTO.Order);
            return Ok(result);

        }

        [HttpPost("paymentSuccessFul")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            var service = new SessionService();
            var sessionDetails =  service.Get(orderHeaderDTO.SessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccess(orderHeaderDTO.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorResponseDTO()
                    {
                        ErrorMessage = "Payment Failed",

                    });
                }
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
