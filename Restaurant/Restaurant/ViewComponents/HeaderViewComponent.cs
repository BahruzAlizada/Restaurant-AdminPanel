using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Models;
using Restaurant.ViewsModel;
using System.Threading.Tasks;

namespace Restaurant.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        public HeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
           AppUser user  = await _userManager.FindByNameAsync(User.Identity.Name);
            HeaderVM header = new HeaderVM
            {

                Username = user.UserName,
                Image = user.Image,
            };

            return View(header);
        }
    }
}
