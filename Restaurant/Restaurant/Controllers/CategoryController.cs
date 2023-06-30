using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin,ComManager")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;

        public CategoryController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            decimal take = 6;
            ViewBag.PageCount = Math.Ceiling((decimal)(await _db.Categories.CountAsync()/take));
            ViewBag.CurrentPage = page;
            List<Category> categories = await _db.Categories.OrderByDescending(x=>x.Id).Skip((page-1)*6).Take((int)(take)).ToListAsync();
            return View(categories);
        }

        [HttpPost]

        public async Task<IActionResult> Index(string name)
        {
            var existingCategory = await _db.Categories.AnyAsync(x => x.Name == name);
            if (existingCategory)
            {
                ViewBag.Message = "Bu isimde bir kategori zaten mevcut.";
               ModelState.AddModelError("Name","Bu isimde bir kategori zaten mevcut.");
                return View();
            }

            Category category = new Category
            {
                Name = name
            };
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> Create(Category category)
        //{
        //    #region Exist
        //    bool isExist = await _db.Categories.AnyAsync(x => x.Name == category.Name);
        //    if (isExist)
        //    {
        //        ModelState.AddModelError("Name", "Bu adda kategoriya hal-hazırda mövcuddur");
        //        return View();
        //    }
        //    #endregion

        //    await _db.Categories.AddAsync(category);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcategory == null)
                return BadRequest();

            return View(dbcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Category category)
        {
            if (id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcategory == null)
                return BadRequest();

            #region Exist
            bool isExist = await _db.Categories.AnyAsync(x => x.Name == category.Name && x.Id != id);
            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kategoriya hal-hazırda mövcuddur");
                return View();
            }
            #endregion

            dbcategory.Name = category.Name;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Category dbcategory = await _db.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (dbcategory == null)
                return BadRequest();

            if (dbcategory.IsDeactive)
                dbcategory.IsDeactive = false;
            else
                dbcategory.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
