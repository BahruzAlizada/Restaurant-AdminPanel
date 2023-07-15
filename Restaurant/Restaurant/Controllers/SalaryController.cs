using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;
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

        #region Index
        public async Task<IActionResult> Index(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var salary = from x in _db.Employees select x;
                List<Employee> employees = await _db.Employees.Where(x=>x.Name.Contains(search)).ToListAsync();
                return View(employees);
            }
            List<Employee> employee = await _db.Employees.ToListAsync();
            return View(employee);
        }
        #endregion

        #region Detail
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id==id);
            if (employee == null)
                return BadRequest();

            string emp = employee.Name + employee.Surname;
            List<Salary> salaries = await _db.Salarys.Where(x => x.Employee.Contains(emp)).OrderByDescending(x=>x.CreatedTime).ToListAsync();
            return View(salaries);
        }
        #endregion
    }
}
