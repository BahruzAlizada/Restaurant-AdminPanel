using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Helpers;
using Restaurant.Models;
using Restaurant.ViewsModel;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Authorize(Roles = "Admin,ComManager")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _env;
        public UserController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,IWebHostEnvironment env)
        {
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            List<AppUser> dbusers = await _userManager.Users.ToListAsync();
            List<UserVM> userVM = new List<UserVM>();

            foreach (AppUser appuser in dbusers)
            {
                UserVM user = new UserVM
                {
                    Id=appuser.Id,
                    FullName = appuser.FullName,
                    Username = appuser.UserName,
                    Email = appuser.Email,
                    Phone = appuser.PhoneNumber,
                    IsDeactive=appuser.IsDeactive,
                    Role = (await _userManager.GetRolesAsync(appuser))[0]
                };
                userVM.Add(user);
            }
            return View(userVM);
        }
        #endregion

        #region Create
        public IActionResult Create()
        {
            ViewBag.Roles = new List<string>
            {
                Helpers.Role.Admin.ToString(),
                Helpers.Role.ComManager.ToString(),
                Helpers.Role.Ofisiant.ToString(),
                Helpers.Role.Admistrator.ToString()
            };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(CreateVM createVM,string role)
        {
            ViewBag.Roles = new List<string>
            {
                Helpers.Role.Admin.ToString(),
                Helpers.Role.ComManager.ToString(),
                Helpers.Role.Ofisiant.ToString(),
                Helpers.Role.Admistrator.ToString()
            };

           

            AppUser newuser = new AppUser
            {
                FullName = createVM.FullName,
                UserName = createVM.UserName,
                Email = createVM.Email,
                PhoneNumber = createVM.PhoneNumber,
                CreatedTime = createVM.CreatedTime,
            };

            IdentityResult result = await _userManager.CreateAsync(newuser, createVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            await _userManager.AddToRoleAsync(newuser, role);

            return RedirectToAction("Index");
        }
        #endregion

        #region Update
        public async Task<IActionResult> Update(string id)
        {
            if (id == null)
                return NotFound();
            AppUser dbuser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (dbuser == null)
                return BadRequest();

            ViewBag.Roles = new List<string>
            {
                Helpers.Role.Admin.ToString(),
                Helpers.Role.ComManager.ToString(),
                Helpers.Role.Ofisiant.ToString(),
                Helpers.Role.Admistrator.ToString()
            };

            UpdateVM updateVM = new UpdateVM
            {
                FullName = dbuser.FullName,
                Email = dbuser.Email,
                Username = dbuser.UserName,
                PhoneNumber = dbuser.PhoneNumber,
                Role = (await _userManager.GetRolesAsync(dbuser))[0]
            };

            return View(updateVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(string id,UpdateVM update,string role)
        {
            #region HttpGet
            if (id == null)
                return NotFound();
            AppUser dbuser = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (dbuser == null)
                return BadRequest();

            ViewBag.Roles = new List<string>
            {
                Helpers.Role.Admin.ToString(),
                Helpers.Role.ComManager.ToString(),
                Helpers.Role.Ofisiant.ToString(),
                Helpers.Role.Admistrator.ToString()
            };

            UpdateVM updateVM = new UpdateVM
            {
                FullName = dbuser.FullName,
                Email = dbuser.Email,
                Username = dbuser.UserName,
                PhoneNumber = dbuser.PhoneNumber,
                Role = (await _userManager.GetRolesAsync(dbuser))[0]
            };
            #endregion

        
            dbuser.FullName=update.FullName;
            dbuser.Email=update.Email;
            dbuser.UserName = update.Username;
            dbuser.PhoneNumber=update.PhoneNumber;

            if (updateVM.Role != role)
            {
                IdentityResult identityResultremove = await _userManager.RemoveFromRoleAsync(dbuser, updateVM.Role);
                if (!identityResultremove.Succeeded)
                {
                    foreach (IdentityError error in identityResultremove.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
                IdentityResult identityResuladd = await _userManager.AddToRoleAsync(dbuser, role);
                if (!identityResultremove.Succeeded)
                {
                    foreach (IdentityError error in identityResultremove.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View();
                }
            }

            await _userManager.UpdateAsync(dbuser);
            return RedirectToAction("Index");

        }
        #endregion

        #region ResetPassword
        public async Task<IActionResult> ResetPassword(string id)
        {
            if (id == null)
                return NotFound();
            AppUser dbuser = await _userManager.FindByIdAsync(id);
            if (dbuser == null)
                return BadRequest();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(string id,ResetPasswordVM resetPassword)
        {
            if (id == null)
                return NotFound();
            AppUser dbuser = await _userManager.FindByIdAsync(id);
            if (dbuser == null)
                return BadRequest();

            string token = await _userManager.GeneratePasswordResetTokenAsync(dbuser);
            IdentityResult result = await _userManager.ResetPasswordAsync(dbuser, token, resetPassword.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region Activity
        public async Task<IActionResult> Activity(string id)
        {
            if (id == null)
                return NotFound();
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return BadRequest();

           if(user.IsDeactive)
                user.IsDeactive=false;
           else
                user.IsDeactive=true;

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
        #endregion
    }
}

