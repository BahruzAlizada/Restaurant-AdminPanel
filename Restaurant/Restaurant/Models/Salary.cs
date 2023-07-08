using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Salary
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public double Amount { get; set; }
        public string Description { get; set; } = "Maaş";
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow.AddHours(4);
        public string By { get; set; }
        public string Employee { get; set; }
    }
}
