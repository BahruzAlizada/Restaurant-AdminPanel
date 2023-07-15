using System.ComponentModel.DataAnnotations;
using System;

namespace Restaurant.Models
{
    public class KitchenBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu Xana boş ola bilməz")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Bu Xana boş ola bilməz")]
        public double Price { get; set; }
        [Required(ErrorMessage = "Bu Xana boş ola bilməz")]
        public double Quantity { get; set; }
        public string By { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
