using System.ComponentModel.DataAnnotations;

namespace Api
{
    public class UpdateProduct
    {
        [StringLength(200, MinimumLength = 2)]
        public string? Title { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
    }
}
