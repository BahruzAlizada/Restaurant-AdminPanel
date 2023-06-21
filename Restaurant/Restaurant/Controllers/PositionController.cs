using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.DAL;
using Restaurant.Models;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class PositionController : Controller
    {
        private readonly AppDbContext _db;
        public PositionController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Position> positions = await _db.Positions.ToListAsync();
            return View(positions);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Position position)
        {
            #region Salary
            if (position.Salary < 0)
            {
                ModelState.AddModelError("Salary", "Maaş miqdarın düzgün daxil edin");
                return View();
            }
            #endregion

            #region Exist
            bool isExist = await _db.Positions.AnyAsync(x => x.Name == position.Name);
            if (isExist)
            {
                ModelState.AddModelError("Name", "BU adda vəzifə hal hazırda mövcuddur");
                return View();
            }
            #endregion

            await _db.Positions.AddAsync(position);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if(id==null)
                return NotFound(); //Change Error page
            Position dbposition = await _db.Positions.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbposition == null)
                return BadRequest();

            return View(dbposition);
        }  

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Position position)
        {
            if (id == null)
                return NotFound(); //Change Error page
            Position dbposition = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
            if (dbposition == null)
                return BadRequest();

            #region Exist
            bool isExist = await _db.Positions.AnyAsync(x=>x.Name==position.Name && x.Id !=position.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu addda vəzifə hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            #region Salary
            if (position.Salary<=0)
            {
                ModelState.AddModelError("Salary", "Maaşı düzgün daxil edin");
                return View();
            }
            #endregion

            dbposition.Name = position.Name;
            dbposition.Salary = position.Salary;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
