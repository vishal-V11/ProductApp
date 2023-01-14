using System.ComponentModel.DataAnnotations;

namespace BookingAPI.Models
{
    public class ProductCart
    {
        [Key]
        public int CartId { get; set; }
        public string ProductName { get; set; }
        public string? Category { get; set; }
        public int Price { get; set; }
        public string? ImagePath { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public int? Total { get; set; }
    }
}
