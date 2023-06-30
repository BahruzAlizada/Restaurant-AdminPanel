using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Controllers;
using Restaurant.DAL;
using Restaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.ViewComponents
{
    public class HomeMoneyViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public HomeMoneyViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Cost> costs = await _db.Costs.ToListAsync();
            List<Profit> profit = await _db.Profits.ToListAsync();
            return View();
        }
    }
}
