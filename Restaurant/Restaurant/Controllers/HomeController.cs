using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        #region Index
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        public IActionResult Error()
        {
            return View();
        }
    }
}
