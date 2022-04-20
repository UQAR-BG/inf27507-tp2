using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace INF27507_Boutique_En_Ligne.Models
{
    public class Product : IModel
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
        public Gender? Gender { get; set; }
        [Required]
        public int UsageId { get; set; }
        public Usage? Usage { get; set; }
        [Required]
        public int ColourId { get; set; }
        [JsonIgnore]
        public Colour? Colour { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        [Required]
        public int SubCategoryId { get; set; }
        [JsonIgnore]
        public SubCategory? SubCategory { get; set; }
        [Required]
        public int ProductTypeId { get; set; }
        [JsonIgnore]
        public ProductType? ProductType { get; set; }
        [Required]
        public int SellerId { get; set; }
        [JsonIgnore]
        public Seller? Seller { get; set; }
    }
}
