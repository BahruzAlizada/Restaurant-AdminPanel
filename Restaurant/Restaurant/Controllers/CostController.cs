using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles ="Admin,ComManager")]
    public class CostController : Controller
    {
        private readonly AppDbContext _db;
        public CostController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index(string search,int page=1)
        {
            List<Cost> costs = new List<Cost>();
            if (!string.IsNullOrEmpty(search))
            {
                var cost = from d in _db.Costs select d;
                List<Cost> result = await _db.Costs.Where(x=>x.Description.Contains(search)).OrderByDescending(x=>x.Id).ToListAsync();
                return View(result);
            }
            decimal take = 6;
            ViewBag.PageCount = Math.Ceiling((decimal)(await _db.Costs.CountAsync() / take));
            ViewBag.CurrentPage = page;
            costs = await _db.Costs.OrderByDescending(x=>x.Id).Skip((page-1)*6).Take(6).ToListAsync();
            return View(costs);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Cost cost)
        {
            #region Amount
            if (cost.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Miqdarı düzgün daxil edin");
                return View();
            }
            #endregion

            cost.By = User.Identity.Name;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.LastModifiedDescription = cost.Description;
            total.LastModifiedTime = cost.CreatedTime;
            total.LastModifiedAmount = cost.Amount;
            total.LastModifiedBy = cost.By;
            total.TotalCash -= cost.Amount;

            await _db.Costs.AddAsync(cost);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Cost dbcost = await _db.Costs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcost == null)
                return BadRequest();

            return View(dbcost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Cost cost)
        {
            if (id == null)
                return NotFound();
            Cost dbcost = await _db.Costs.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcost == null)
                return BadRequest();

            #region Amount
            if (cost.Amount < 0)
            {
                ModelState.AddModelError("Amount", "Miqdarı düzgün qeyd edin");
                return View();
            }
            #endregion

            dbcost.CreatedTime = cost.CreatedTime;
            dbcost.Description = cost.Description;
            dbcost.Amount = cost.Amount;
            dbcost.By=cost.By;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.TotalCash += dbcost.Amount;
            total.TotalCash -= cost.Amount;
            total.LastModifiedTime = cost.CreatedTime;
            total.LastModifiedDescription=cost.Description;
            total.LastModifiedBy=cost.By;
            total.LastModifiedAmount=cost.Amount;
            
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
