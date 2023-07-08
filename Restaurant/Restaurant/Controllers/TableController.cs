using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles ="Admin,ComManager")]
    public class TableController : Controller
    {
        private readonly AppDbContext _db;
        public TableController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<Table> tables = await _db.Tables.ToListAsync();
            return View(tables);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Table table)
        {
            #region Exist
            bool isExist=await _db.Tables.AnyAsync(x=>x.Name == table.Name);
            if(isExist)
            {
                ModelState.AddModelError("Name", "Bu adda masa var.Başqa ad seçin");
                return View();
            }
            #endregion

            await _db.Tables.AddAsync(table);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Table dbtable = await _db.Tables.FirstOrDefaultAsync(x => x.Id == id);
            if (dbtable == null)
                return BadRequest();

            return View(dbtable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Table table)
        {
            if (id == null)
                return NotFound();
            Table dbtable = await _db.Tables.FirstOrDefaultAsync(x => x.Id == id);
            if (dbtable == null)
                return BadRequest();

            #region Exist
            bool isExist = await _db.Tables.AnyAsync(x => x.Name == table.Name && x.Id!=table.Id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda masa var.Başqa ad seçin");
                return View();
            }
            #endregion

            dbtable.Name = table.Name;
            dbtable.ForTwoPerson = table.ForTwoPerson;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
