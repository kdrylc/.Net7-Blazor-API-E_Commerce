using E_Commerce_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace E_Commerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripePaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] StripePaymentDTO paymentDTO)
        {



            try
            {

                var adress = "";
                var adress1 = "";
                var domain = _configuration.GetValue<string>("E_Commerce_URL");
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + paymentDTO.SuccessUrl,
                    CancelUrl = domain + paymentDTO.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                adress = options.SuccessUrl;
                adress1 = options.CancelUrl;
                foreach (var item in paymentDTO.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100) - ((long)(item.Price *100)* paymentDTO.Discount)/100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            },

                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                var service = new SessionService();
                Session session = service.Create(options);

                return Ok(new SuccessResponseDTO()
                {
                    Data = session.Id + ";" + session.PaymentIntentId
                });





            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorResponseDTO()
                {
                    ErrorMessage = ex.Message
                });
            }
        }

    }
}
