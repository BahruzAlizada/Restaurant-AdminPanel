using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    [Authorize(Roles ="Admin,Admistrator")]
    public class KitchenController : Controller
    {
        private readonly AppDbContext _db;
        public KitchenController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index(string search,int page=1)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var kitchen = from x in _db.Kitchens select x;
                List<Kitchen> kitchenss = await _db.Kitchens.Where(x=>x.Name.Contains(search)).ToListAsync();
                return View(kitchenss);
            }
            decimal take = 25;
            ViewBag.PageCount = Math.Ceiling((decimal)(await _db.Kitchens.CountAsync() / take));
            ViewBag.CurrentPage = page;

            List<Kitchen> kitchens = await _db.Kitchens.OrderByDescending(x => x.Id).
                Skip((1 - page) * 25).Take((int)take).ToListAsync();
            return View(kitchens);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Kitchen kitchen)
        {
            #region Quantity
            if (kitchen.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Məlumatı düzgün daxil edin");
                return View();
            }
            #endregion

            #region Price
            if (kitchen.Price <= 0)
            {
                ModelState.AddModelError("Price", "Məlumatı düzgün daxil edin");
                return View();
            }
            #endregion

            kitchen.By = User.Identity.Name;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.LastModifiedAmount = kitchen.Price;
            total.LastModifiedBy = kitchen.By;
            total.TotalCash -= kitchen.Price;
            total.LastModifiedTime = kitchen.CreatedTime;
            total.LastModifiedDescription = "mətbəx";

            await _db.Kitchens.AddAsync(kitchen);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Kitchen dbkitchen = await _db.Kitchens.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbkitchen == null)
                return BadRequest();

            return View(dbkitchen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Kitchen kitchen)
        {
            if (id == null)
                return NotFound();
            Kitchen dbkitchen = await _db.Kitchens.FirstOrDefaultAsync(x => x.Id == id);
            if (dbkitchen == null)
                return BadRequest();

            #region Quantity
            if (kitchen.Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Məlumatı düzgün daxil edin");
                return View();
            }
            #endregion

            #region Price
            if (kitchen.Price <= 0)
            {
                ModelState.AddModelError("Price", "Məlumatı düzgün daxil edin");
                return View();
            }
            #endregion

            dbkitchen.Name=kitchen.Name;
            dbkitchen.Price = kitchen.Price;
            dbkitchen.Quantity=kitchen.Quantity;
            dbkitchen.CreatedTime = kitchen.CreatedTime;
            dbkitchen.Description = kitchen.Description;
            kitchen.By = User.Identity.Name;

            Total total = await _db.Totals.FirstOrDefaultAsync();
            total.LastModifiedAmount = kitchen.Price;
            total.LastModifiedBy = kitchen.By;
            total.LastModifiedTime = kitchen.CreatedTime;
            total.LastModifiedDescription = "mətbəx";
            total.TotalCash += dbkitchen.Price;
            total.TotalCash -= kitchen.Price;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

    }
}
