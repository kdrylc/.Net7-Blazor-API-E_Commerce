using E_Commerce_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Models
{
    public class OrderDTO
    {
        public OrderHeaderDTO OrderHeader { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
