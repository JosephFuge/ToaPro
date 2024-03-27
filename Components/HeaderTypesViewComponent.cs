using Microsoft.AspNetCore.Mvc;

namespace ToaPro.Components
{
    public class HeaderTypesViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title)
        {
            return View("Default", title);
        }
    }
}