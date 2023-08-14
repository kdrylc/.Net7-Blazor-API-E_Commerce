using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter category name")]
        public string? Name { get; set; }
    }
}
