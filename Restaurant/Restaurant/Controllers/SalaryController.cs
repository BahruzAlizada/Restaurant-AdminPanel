using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class SalaryController : Controller
    {
        private readonly AppDbContext _db;
        public SalaryController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Salary> salaries = await _db.Salarys.ToListAsync();
            return View(salaries);
        }
    }
}
