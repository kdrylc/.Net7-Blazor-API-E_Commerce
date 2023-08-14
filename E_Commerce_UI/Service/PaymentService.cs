using E_Commerce_Models;
using E_Commerce_UI.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace E_Commerce_UI.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SuccessResponseDTO> Checkout(StripePaymentDTO model)
        {
            try
            {
                var content = JsonConvert.SerializeObject(model);
                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/StripePayment/create", bodyContent);
                string responseResult = response.Content.ReadAsStringAsync().Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<SuccessResponseDTO>(responseResult);
                    return result;
                }
                else
                {
                    var errorModel = JsonConvert.DeserializeObject<ErrorResponseDTO>(responseResult);
                    throw new Exception(errorModel.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
