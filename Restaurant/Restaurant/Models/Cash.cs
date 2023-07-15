using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class Cash
    {
        [Key]
        public int Id { get; set; }
        public double Price { get; set; }
        public bool Status { get; set; }
        public List<CashProduct> CashProducts { get; set; }
        public DateTime CreatedTime { get; set; }=DateTime.UtcNow.AddHours(4);
        public Table Table { get; set; }
        public int TableId { get; set; }
    }
}
