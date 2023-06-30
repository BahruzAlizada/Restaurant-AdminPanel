using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Restaurant.DAL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin,ComManager")]
    public class ProfitController : Controller
    {
        private readonly AppDbContext _db;
        public ProfitController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index(string search, int page=1)
        {
            List<Profit> profit = new List<Profit>();

            if (!string.IsNullOrEmpty(search))
            {
               var  profitt = from d in _db.Profits select d;
                profit = await _db.Profits.Where(x=>x.Description.Contains(search)).OrderByDescending(x=>x.Id).ToListAsync();
                return View(profit);
            }

            decimal take = 6;
            ViewBag.PageCount = Math.Ceiling((decimal)(await _db.Profits.CountAsync() / take));
            ViewBag.CurrentPage = page;

            profit = await _db.Profits.OrderByDescending(x=>x.Id).Skip((page-1)*6).Take((int)take).ToListAsync();
            return View(profit);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Profit profit)
        {
            #region Amonunt
            if (profit.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Gəliri düzgün daxil edin");
                return View();
            }
            #endregion

            profit.By = User.Identity.Name;

            Total total = await _db.Totals.FirstOrDefaultAsync();

            total.LastModifiedAmount = profit.Amount;
            total.LastModifiedDescription = profit.Description;
            total.LastModifiedBy = profit.By;
            total.LastModifiedTime = profit.CreatedTime;
            total.TotalCash += profit.Amount;

            await _db.Profits.AddAsync(profit);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Profit dbprofit = await _db.Profits.FirstOrDefaultAsync(x => x.Id == id);
            if (dbprofit == null)
                return BadRequest();

            return View(dbprofit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Profit profit)
        {
            if (id == null)
                return NotFound();
            Profit dbprofit = await _db.Profits.FirstOrDefaultAsync(x => x.Id == id);
            if (dbprofit == null)
                return BadRequest();

            #region Amount
            if (profit.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Miqdarı düzgüün şəkildə qeyd edin");
                return View();
            }
            #endregion

            dbprofit.By = profit.By;
            dbprofit.Amount = profit.Amount;
            dbprofit.CreatedTime = profit.CreatedTime;
            dbprofit.Description = profit.Description;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.TotalCash-= dbprofit.Amount;
            total.TotalCash += profit.Amount;
            total.LastModifiedTime = profit.CreatedTime;
            total.LastModifiedBy = profit.By;
            total.LastModifiedDescription=profit.Description;
            total.LastModifiedAmount = profit.Amount;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
