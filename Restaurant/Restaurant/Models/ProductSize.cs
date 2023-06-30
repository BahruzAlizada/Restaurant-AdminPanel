using System.Collections.Generic;

namespace Restaurant.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public List<Product> Products { get; set; }
        public List<SpecialMenu> SpecialMenus { get; set; }
    }
}
