using Microsoft.AspNetCore.Mvc;
using ToaPro.Models.ViewModels;

namespace ToaPro.Components
{
    public class NotificationDialogViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string body)
        {
            var dialogModel = new NotificationDialogViewModel { Title = title, Body = body };
            return View("Default", dialogModel);
        }
    }
}
