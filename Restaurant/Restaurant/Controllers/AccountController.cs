using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Controllers
{
    public class AccountController : Controller
    {
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        #endregion

        #region Profile
        public IActionResult Profile()
        {
            return View();
        }
        #endregion
    }
}
