using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Components
{
    public class NavBarTypesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string user)
        {
            return View(user);
        }
    }
}