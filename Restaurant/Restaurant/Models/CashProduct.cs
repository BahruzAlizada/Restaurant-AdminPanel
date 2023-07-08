namespace Restaurant.Models
{
    public class CashProduct
    {
        public int Id { get; set; }   
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Cash Cash { get; set; }
        public int CashId { get; set; }
    }
}
