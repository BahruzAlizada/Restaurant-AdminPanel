using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.DAL;
using Restaurant.Models;
using Restaurant.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.ViewComponents
{
    public class OrderandSalaryHomeViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public OrderandSalaryHomeViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var employee = await _db.Employees.Include(x=>x.Position).ToListAsync();
            double salary = 0;
            foreach (var item in employee)
            {
                salary += item.Position.Salary;
            }

            var start = DateTime.Today;
            var end = DateTime.Today.AddDays(-1);

            var cash = await _db.Cashes.Where(x=>x.CreatedTime>=end && x.CreatedTime<=start).CountAsync();

            OrderandSalaryVM orderandSalary = new OrderandSalaryVM
            {

                TotalSalary = salary,
                DayTotalCash = cash
            };

            return View(orderandSalary);
        }
    }
}
