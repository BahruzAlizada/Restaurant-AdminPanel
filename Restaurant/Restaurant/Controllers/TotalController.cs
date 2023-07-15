using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.Models;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TotalController : Controller
    {
        private readonly AppDbContext _db;
        public TotalController(AppDbContext db)
        {
            _db = db;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            Total total = await _db.Totals.FirstOrDefaultAsync();
            return View(total);
        }
        #endregion
    }
}
