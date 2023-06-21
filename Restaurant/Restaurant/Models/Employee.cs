using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public DateTime Brith { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
    }
}
