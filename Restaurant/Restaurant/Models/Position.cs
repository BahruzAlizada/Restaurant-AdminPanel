using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Position
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public double Salary { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
