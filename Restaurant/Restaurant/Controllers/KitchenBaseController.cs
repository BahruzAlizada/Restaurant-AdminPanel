using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles ="Admistrator,Admin")]
    public class KitchenBaseController : Controller
    {
        private readonly AppDbContext _db;
        public KitchenBaseController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<KitchenBase> kitchenbase = await _db.KitchenBases.ToListAsync();
            return View(kitchenbase);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(KitchenBase kitchenbase)
        {
            #region Quantity
            if (kitchenbase.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Məlumatı düzgün daxil edin");
                return View();
            }
            #endregion

            #region Price
            if (kitchenbase.Price <= 0)
            {
                ModelState.AddModelError("Price", "Məlumatı düzgün daxil edin");
                return View();
            }
            #endregion

            kitchenbase.By = User.Identity.Name;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.LastModifiedAmount = kitchenbase.Price;
            total.LastModifiedBy = kitchenbase.By;
            total.TotalCash -= kitchenbase.Price;
            total.LastModifiedTime = kitchenbase.CreatedTime;
            total.LastModifiedDescription = "mətbəx";

            await _db.KitchenBases.AddAsync(kitchenbase);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        #endregion

        #region Increase
        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
                return NotFound();
            KitchenBase kitchenBase = await _db.KitchenBases.FirstOrDefaultAsync(x => x.Id == id);
            if (kitchenBase == null)
                return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Increase(int? id, double quantity,double price)
        {
            if (id == null)
                return NotFound();
            KitchenBase kitchenBase = await _db.KitchenBases.FirstOrDefaultAsync(x => x.Id == id);
            if (kitchenBase == null)
                return BadRequest();

            kitchenBase.Quantity += quantity;
            kitchenBase.Price += price;
            kitchenBase.CreatedTime = DateTime.UtcNow.AddHours(4);
            kitchenBase.By = User.Identity.Name;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.LastModifiedBy = kitchenBase.By;
            total.LastModifiedDescription = "Mətbəx əsas şeylər";
            total.LastModifiedTime = kitchenBase.CreatedTime;
            total.LastModifiedAmount = price;
            total.TotalCash += price;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Decrease
        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
                return NotFound();
            KitchenBase kitchenbase = await _db.KitchenBases.FirstOrDefaultAsync(x => x.Id == id);
            if (kitchenbase == null)
                return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Decrease(int? id,double quantity)
        {
            if (id == null)
                return NotFound();
            KitchenBase kitchenbase = await _db.KitchenBases.FirstOrDefaultAsync(x => x.Id == id);
            if (kitchenbase == null)
                return BadRequest();

            kitchenbase.Quantity -= quantity;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
