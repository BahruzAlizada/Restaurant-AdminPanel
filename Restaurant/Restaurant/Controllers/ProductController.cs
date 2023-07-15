using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Helpers;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin,ComManager")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDbContext db, IWebHostEnvironment env)
        {
            _env = env;
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index(string search,int page=1)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var product = from x in _db.Products select x;
                var Product = await _db.Products.Where(x=>x.Name.Contains(search)).Include(x => x.Category).Include(x => x.ProductSize).ToListAsync();
                return View(Product);
            }
            decimal take = 10;
            ViewBag.PageCount=Math.Ceiling((decimal)(await _db.Products.Where(x=>!x.IsDeactive).CountAsync()/take));
            ViewBag.CurrentPage = page;

            List<Product> products = await _db.Products.Include(x => x.Category).Include(x => x.ProductSize).
                OrderByDescending(x => x.Id).Skip((page - 1) * 10).Take((int)take).ToListAsync();
            return View(products);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Sizes = await _db.ProductSizes.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Product product, int categoryId, int sizeId)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Sizes = await _db.ProductSizes.ToListAsync();

            #region Image
            if (product.Photo == null)
            {
                ModelState.AddModelError("Photo", "Bu xana boş ola bilməz");
                return View();
            }
            if (!product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sadəcə şəkil tipli");
                return View();
            }
            if (product.Photo.IsOlder512Kb())
            {
                ModelState.AddModelError("Photo", "Maksimum ölçü 512 Kb olmalıdır");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "assets", "img", "product");
            product.Image = await product.Photo.SaveFileAsync(folder);
            #endregion

            product.CategoryId = categoryId;
            product.ProductSizeId = sizeId;

            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Sizes = await _db.ProductSizes.ToListAsync();

            if (id == null)
                return NotFound();
            Product dbproduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbproduct == null)
                return BadRequest();

            return View(dbproduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id, int categoryId, int sizeId, Product product)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Sizes = await _db.ProductSizes.ToListAsync();

            if (id == null)
                return NotFound();
            Product dbproduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbproduct == null)
                return BadRequest();

            #region Image
            if(product.Photo != null)
            {
                if (!product.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Sadəcə şəkil tipli ");
                    return View();
                }
                if(product.Photo.IsOlder512Kb())
                {
                    ModelState.AddModelError("Photo", "Maksimum 512 Kb ");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "assets", "img", "product");
                product.Image = await product.Photo.SaveFileAsync(folder);
                string path = Path.Combine(_env.WebRootPath, folder, dbproduct.Image);
                if(System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
                dbproduct.Image = product.Image;
            }
            #endregion

            dbproduct.CategoryId = categoryId;
            dbproduct.ProductSizeId = sizeId;
            dbproduct.Name = product.Name;
            dbproduct.Description=product.Description;
            dbproduct.Price = product.Price;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
                return NotFound();
            Product dbproduct = await _db.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (dbproduct == null)
                return BadRequest();

            if (dbproduct.IsDeactive)
                dbproduct.IsDeactive = false;
            else
                dbproduct.IsDeactive = true;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
