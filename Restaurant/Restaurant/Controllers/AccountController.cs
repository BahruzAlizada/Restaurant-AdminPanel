using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Models;
using Restaurant.ViewsModel;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [AllowAnonymous]    
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(RoleManager<IdentityRole> roleManager,UserManager<AppUser> userManager
            ,SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Login
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginVM login)
        {
            AppUser user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Email yaxud şifrə yanlışdır.");
                return View();
            }
            if (user.IsDeactive)
            {
                ModelState.AddModelError("", "Sənin hesabın deaktivdir");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, login.IsRemember, true);
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("","Email və ya Şifrə yanlışdır");
                return View();
            }

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Sizin hesab bloklanıb");
                return View();
            }
            if (await _userManager.IsInRoleAsync(user,Helpers.Role.Admin.ToString()))
                return RedirectToAction("Index", "Home");
            else
                return RedirectToAction("Index", "Cash");

        }
        #endregion

        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion


        #region ForgetPassword
        public  IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM forgetPassword)
        {
            AppUser user = await _userManager.FindByEmailAsync(forgetPassword.Email);
            string passwordrResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var tokenlink = Url.Action("ResetPassword", "Account", new
            {
                userId=user.Id,
                token=passwordrResetToken
            },HttpContext.Request.Scheme); // Gmailə göndəriləcək linki hazırladıq

            await Helpers.SendEmail.SendMailAsync("Şifrəmi Unutdum", tokenlink, forgetPassword.Email);

            return View();
        }
        #endregion

        #region ResetPassword
        public IActionResult ResetPassword(string userId,string token)
        {
            TempData["userId"] = userId;
            TempData["token"]= token;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPassword)
        {
            var userId = TempData["userId"];
            var token = TempData["token"];
            if (userId == null || token == null)
            {
                ModelState.AddModelError("", "Error");
                return View();
            }

            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.ResetPasswordAsync(user,token.ToString(), resetPassword.Password);
            if (!result.Succeeded)
                return View();
            
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region CreateRoles
        //public async Task CreateRoles()
        //{
        //    if (!await _roleManager.RoleExistsAsync(Helpers.Role.Ofisiant.ToString()))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helpers.Role.Ofisiant.ToString() });
        //    }
        //    if (!await _roleManager.RoleExistsAsync(Helpers.Role.ComManager.ToString()))
        //    {
        //        await _roleManager.CreateAsync(new IdentityRole { Name = Helpers.Role.ComManager.ToString() });
        //    }
        //}
        #endregion
    }
}
