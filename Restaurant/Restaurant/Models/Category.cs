using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Bu xana boş ola bilməz")]
        public string Name { get; set; }
        public bool IsDeactive { get; set; }
        public List<Product> Products { get; set; }
        public List<SpecialMenu> SpecialMenus { get; set; }

    }
}
