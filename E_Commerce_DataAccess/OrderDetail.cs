using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_DataAccess
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        [NotMapped]
        public virtual Product Product { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public string ProductName { get; set; }


    }
}
