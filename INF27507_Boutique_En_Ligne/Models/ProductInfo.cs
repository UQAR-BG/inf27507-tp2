using System.ComponentModel.DataAnnotations;

namespace INF27507_Boutique_En_Ligne.Models;

public class ProductInfo
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Image { get; set; }
    public string ImageURL { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public bool Active { get; set; }
    [Required]
    public int GenderId { get; set; }
    [Required]
    public int UsageId { get; set; }
    [Required]
    public int ColourId { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    public int SubCategoryId { get; set; }
    [Required]
    public int ProductTypeId { get; set; }
}