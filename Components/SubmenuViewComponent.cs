using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Components
{
    public class SubmenuTypesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string view)
        {
            return View(view);
        }
    }
}