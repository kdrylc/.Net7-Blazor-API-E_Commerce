using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_DataAccess
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public int DiscountAmount { get; set; }
    }
}
