using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Table
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Name { get; set; }
        public bool ForTwoPerson { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Cash> Cashs { get; set; }
    }
}
