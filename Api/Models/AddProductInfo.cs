using System.ComponentModel.DataAnnotations;

namespace Api
{
    public class AddProductInfo
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string ImageURL { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public Usage Usage { get; set; }
        [Required]
        public Colour Colour { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public SubCategory SubCategory { get; set; }
        [Required]
        public ProductType ProductType { get; set; }
    }
}
