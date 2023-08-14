using E_Commerce_Models;
using System.ComponentModel.DataAnnotations;

public class DetailsVM
{
    public DetailsVM()
    {
        ProductPrice = new();
        Count = 1;
    }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value greater than 0")]
    public int Count { get; set; }
    [Required]
    public int SelectedProductPriceId { get; set; }
    public ProductPriceDTO ProductPrice { get; set; }
}