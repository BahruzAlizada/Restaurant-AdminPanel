using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin,ComManager")]
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _db;
        public EmployeeController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _db.Employees.Include(x=>x.Position).ToListAsync();
            return View(employees);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(int positionId, Employee employee)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            #region Phone
            if (employee.Phone.Length != 10)
            {
                ModelState.AddModelError("Phone", "Düzgün telefon nömrəsi daxil edin");
                return View();
            }
            #endregion

            employee.PositionId = positionId;
            await _db.Employees.AddAsync(employee);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            if (id == null)
                return NotFound();
            Employee dbemployee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (dbemployee == null)
                return BadRequest();

            return View(dbemployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,int positionId,Employee employee)
        {
            ViewBag.Positions = await _db.Positions.ToListAsync();

            if (id == null)
                return NotFound();
            Employee dbemployee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (dbemployee == null)
                return BadRequest();

            #region Phone
            if (employee.Phone.Length != 10)
            {
                ModelState.AddModelError("Phone", "Düzgün telefon nömrəsi daxil edin");
                return View();
            }
            #endregion

            dbemployee.PositionId = positionId;
            dbemployee.Name = employee.Name;
            dbemployee.Surname = employee.Surname;
            dbemployee.Phone = employee.Phone;
            dbemployee.Email = employee.Email;
            dbemployee.Brith= employee.Brith;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = _db.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
                return BadRequest();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public IActionResult DeletePost(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = _db.Employees.FirstOrDefault(x => x.Id == id);
            if (employee == null)
                return BadRequest();

            _db.Remove(employee);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region Salary
        public async Task<IActionResult> Salary(int? id)
        {
            Employee employee = await _db.Employees.Include(x=>x.Position).FirstOrDefaultAsync(x => x.Id == id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Salary")]
        public async Task<IActionResult> SalaryPost(int? id)
        {       
            Employee employee = await _db.Employees.Include(x=>x.Position).FirstOrDefaultAsync(x => x.Id == id);
            Salary salary = new Salary();
            salary.Employee = employee.Name + employee.Surname;
            salary.By = User.Identity.Name;
            salary.Amount = employee.Position.Salary;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.TotalCash -= salary.Amount;
            total.LastModifiedTime = salary.CreatedTime;
            total.LastModifiedBy = salary.By;
            total.LastModifiedAmount = salary.Amount;
            total.LastModifiedDescription = "Maaş";

            await _db.Salarys.AddAsync(salary);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region SendEmail
        public async Task<IActionResult> SendEmail(int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _db.Employees.FirstOrDefaultAsync(x=>x.Id == id);
            if (employee == null)
                return BadRequest();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SendEmail(string subject,string message,int? id)
        {
            if (id == null)
                return NotFound();
            Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee == null)
                return BadRequest();

            #region Message
            if(message == null)
            {
                ModelState.AddModelError("Message", "Mesaj hissəsini doldurmalısınız");
                return View();
            }

            #endregion

            await Helpers.SendEmail.SendMailAsync(subject, message, employee.Email);
            return RedirectToAction("Index");
        }

        #endregion

        #region SendAllEmail
        public IActionResult SendAllEmail()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SendAllEmail(string subject,string message)
        {
            List<Employee> employees = await _db.Employees.ToListAsync();  

            foreach (Employee item in employees)
            {
                await Helpers.SendEmail.SendMailAsync(subject,message, item.Email);
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}
