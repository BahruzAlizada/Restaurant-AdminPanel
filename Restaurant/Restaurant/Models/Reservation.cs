using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Bu xana boş ola bilməz")]
        public DateTime Time { get; set; }
        public Table Table { get; set; }
        public int TableId { get; set; }
    }
}
