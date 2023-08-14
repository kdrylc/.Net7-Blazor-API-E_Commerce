using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Commerce_DataAccess;

namespace E_Commerce_Models
{
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]

        public virtual ProductDTO Product { get; set; }
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
