using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Controllers;
using Restaurant.DAL;
using Restaurant.Models;
using Restaurant.ViewsModel;
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
            double totalcost=0;
            foreach (var cost in costs)
            {
                totalcost += cost.Amount;
            };
            List<Profit> profits = await _db.Profits.ToListAsync();
            double totalprofit = 0;
            foreach (var profit in profits)
            {
                totalprofit += profit.Amount;
            }

            HomeMoneyVM homeMoney = new HomeMoneyVM
            {
                TotalCost = totalcost,
                TotalProfit = totalprofit
            };
            return View(homeMoney);
        }
    }
}
