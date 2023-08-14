using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Models
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO()
        {
            SuccessUrl = "SuccessOrder";
            CancelUrl = "Summary";
        }

        public OrderDTO Order { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public int Discount { get; set; }
    }
}
