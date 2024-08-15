using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToaPro.Models;

namespace ToaPro.Components
{
    public class NavBarTypesViewComponent : ViewComponent
    {
        UserManager<ToaProUser> _userManager;
        public NavBarTypesViewComponent(UserManager<ToaProUser> tempUserManager) {
            _userManager = tempUserManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userClaim = HttpContext.User;
            if (userClaim != null)
            {
                var user = await _userManager.GetUserAsync(userClaim);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    string userRole = roles.FirstOrDefault() ?? "Default";
                    return View(userRole);
                }
                
            }
            
            return View("Default");
        }
    }
}