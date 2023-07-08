using Restaurant.Models;
using System.Collections.Generic;

namespace Restaurant.ViewsModel
{
    public class CashIndexVM
    {
        public List<Table> Tables { get;set; }
        public List<Cash> Cashes { get;set; }
        public Cash Cash { get;set; }
    }
}
