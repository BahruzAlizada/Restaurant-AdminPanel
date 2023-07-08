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
    [Authorize(Roles ="Admin,ComManager")]
    public class ReservationController : Controller
    {
        private readonly AppDbContext _db;
        public ReservationController(AppDbContext db)
        {
            _db = db;
        }
        #region Index
        public async Task<IActionResult> Index(int page=1)
        {
            decimal take = 10;
            ViewBag.Current = page;
            ViewBag.PageCount=Math.Ceiling((decimal)(await _db.Reservations.CountAsync()/take));

            List<Reservation> reservations = await _db.Reservations.Include(x=>x.Table).OrderByDescending(x=>x.Id).
                                             Skip((1-page)*10).Take((int)10).ToListAsync();
            return View(reservations);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Tables=await _db.Tables.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Reservation reservation,int tableId)
        {
            ViewBag.Tables = await _db.Tables.ToListAsync();

            #region Phone
            if (reservation.Phone.Length > 10)
            {
                ModelState.AddModelError("Phone", "Düzgün telefon nömrəsi daxil edin");
                return View();
            }
            #endregion

            reservation.TableId = tableId;

            await _db.Reservations.AddAsync(reservation);  
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(int? id)
        {
            ViewBag.Tables = await _db.Tables.ToListAsync();

            if (id == null)
                return NotFound();
            Reservation dbreservation =await _db.Reservations.FirstOrDefaultAsync(x=>x.Id==id);
            if (dbreservation == null)
                return BadRequest();

            return View(dbreservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Reservation reservation,int tableId)
        {
            ViewBag.Tables = await _db.Tables.ToListAsync();

            if (id == null)
                return NotFound();
            Reservation dbreservation = await _db.Reservations.FirstOrDefaultAsync(x => x.Id == id);
            if (dbreservation == null)
                return BadRequest();

            dbreservation.TableId = tableId;
            dbreservation.Name=reservation.Name;
            dbreservation.Phone=reservation.Phone;
            dbreservation.Description = reservation.Description;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        #endregion

        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();
            Reservation dbreservation = _db.Reservations.FirstOrDefault(x => x.Id == id);
            if (dbreservation == null)
                return BadRequest();

            return View(dbreservation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]

        public IActionResult DeletePost(int? id)
        {
            if (id == null)
                return NotFound();
            Reservation dbreservation = _db.Reservations.FirstOrDefault(x => x.Id == id);
            if (dbreservation == null)
                return BadRequest();

            _db.Reservations.Remove(dbreservation);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        #endregion
    }
}
