using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter the name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter the description")]

        public string Description { get; set; }
        public bool ShopFavourites { get; set; }
        public bool CustomerFavourites { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please Select Category")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public ICollection<ProductPriceDTO> ProductPrices { get; set; }
    }
}
