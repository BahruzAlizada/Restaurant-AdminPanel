using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using Restaurant.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles="Ofisiant,Admin,ComManager")]
    public class CashController : Controller
    {
        private readonly AppDbContext _db;
        public CashController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            List<Cash> cashes = await _db.Cashes.Include(x => x.Table).Include(x => x.CashProducts).ThenInclude(x=>x.Product).OrderByDescending(x => x.Id).ToListAsync();
            List<Table> tables = await _db.Tables.Include(x=>x.Cashs).ToListAsync();
            foreach (var item in cashes)
            {
                foreach (var product in item.CashProducts)
                {
                    item.Price += product.Product.Price;
                }
                item.Price += item.Price * 3 / 100;   
            }

            

            CashIndexVM cashindex = new CashIndexVM
            {
                Tables = tables,
                Cashes = cashes
            };
            
            

            return View(cashindex);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create(int id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Products = await _db.Products.ToListAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(int[] productsId, int[] quantities, int id)
        {
            ViewBag.Categories = await _db.Categories.ToListAsync();
            ViewBag.Products = await _db.Products.ToListAsync();

            Cash cash = new Cash();

            #region CashProduct
            List<CashProduct> cashProducts = new List<CashProduct>();
            foreach (int productId in productsId)
            {
                CashProduct cashProduct = new CashProduct
                {
                    CashId = cash.Id,
                    ProductId = productId
                };
                cashProducts.Add(cashProduct);
            }
            cash.CashProducts = cashProducts;
            #endregion


            cash.Status = false;
            cash.TableId = id;

            await _db.Cashes.AddAsync(cash);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Tables = await _db.Tables.ToListAsync();
            ViewBag.Products = await _db.Products.ToListAsync();

            if (id == null)
                return NotFound();
            Cash dbcash = await _db.Cashes.Include(x=>x.CashProducts).ThenInclude(x=>x.Product).FirstOrDefaultAsync(x=>x.Id== id);
            if (dbcash == null)
                return BadRequest();

            return View(dbcash);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,int tableId, int[] productsId,Cash cash)
        {
            ViewBag.Tables = await _db.Tables.ToListAsync();
            ViewBag.Products = await _db.Products.ToListAsync();

            if (id == null)
                return NotFound();
            Cash dbcash = await _db.Cashes.Include(x => x.CashProducts).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            if (dbcash == null)
                return BadRequest();

            dbcash.TableId= tableId;

            #region CashProduct
            List<CashProduct> cashProducts = new List<CashProduct>();
            foreach (int productId in productsId)
            {
                CashProduct cashProduct = new CashProduct
                {
                    CashId = dbcash.Id,
                    ProductId = productId
                };
                cashProducts.Add(cashProduct);
            }
            dbcash.CashProducts = cashProducts;
            #endregion

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Status
        public async Task<IActionResult> Status(int? id)
        {
            if (id == null)
                return NotFound();
            Cash cash = await _db.Cashes.FirstOrDefaultAsync(x=>x.Id == id);
            if (cash == null)
                return BadRequest();

            return View(cash);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Status")]

        public async Task<IActionResult> StatusPost(int? id)
        {
            if (id == null)
                return NotFound();
            Cash cash = await _db.Cashes.FirstOrDefaultAsync(x => x.Id == id);
            if (cash == null)
                return BadRequest();

            if (cash.Status == false)
                cash.Status = true;
            if(cash.Status== true)
            {
                Total total = await _db.Totals.FirstOrDefaultAsync();
                total.TotalCash += cash.Price;
                total.LastModifiedDescription = "Kassa";
                total.LastModifiedBy = User.Identity.Name;
                total.LastModifiedTime = DateTime.UtcNow.AddHours(4);
                total.LastModifiedAmount = cash.Price;
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Cash cash = _db.Cashes.FirstOrDefault(x=>x.Id== id);
            if (cash == null)
                return BadRequest();

            _db.Cashes.Remove(cash);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion

        #region GetProductByCategory
        public async Task<IActionResult> GetProductsByCategory(int categoryId)
        {
            List<Product> products = await _db.Products.Include(x => x.ProductSize).Where(x => x.CategoryId == categoryId).ToListAsync();

            return PartialView("_ProductList", products);
        }
        #endregion

        #region Check
        public async Task<IActionResult> Check(int id)
        {
            var products = await _db.Products.Where(x=>x.Id==id).Include(x => x.ProductSize).Include(x => x.Category).ToListAsync();
            return View();
        }
        #endregion

        #region Calculator

        #endregion

        #region CreateeCooments
        //public async Task<IActionResult> Create(int id)
        //{
        //    ViewBag.Tables=await _db.Tables.ToListAsync();
        //    ViewBag.Categories = await _db.Categories.ToListAsync();
        //    ViewBag.Products = await _db.Products.ToListAsync();

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public async Task<IActionResult> Create(int[] productsId, int[] quantities,int id)
        //{
        //    ViewBag.Tables = await _db.Tables.ToListAsync();
        //    ViewBag.Categories = await _db.Categories.ToListAsync();
        //    ViewBag.Products = await _db.Products.ToListAsync();

        //    Cash cash = new Cash();

        //    #region CashProduct
        //    List<CashProduct> cashProducts = new List<CashProduct>();
        //    foreach (int productId in productsId)
        //    {
        //        CashProduct cashProduct = new CashProduct
        //        {
        //            CashId = cash.Id,
        //            ProductId = productId
        //        };
        //        cashProducts.Add(cashProduct);
        //    }
        //    cash.CashProducts = cashProducts;
        //    #endregion


        //    cash.Status = false;
        //    cash.TableId = id;

        //    await _db.Cashes.AddAsync(cash);
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
        #endregion
    }
}
