using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public double Price { get;set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool IsDeactive { get; set; }
        public ProductSize ProductSize { get; set; }
        public int ProductSizeId { get; set; }
    }
}
