using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Restaurant.DAL;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles ="Admin,ComManager")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Search
        public async Task<IActionResult> Search(string search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                if (search == "Məhsul")
                    return RedirectToAction("Index", "Product");
                else if (search == "Kateqoriya")
                    return RedirectToAction("Index", "Category");
            }

            return View ();
        }
        #endregion
    }
}
