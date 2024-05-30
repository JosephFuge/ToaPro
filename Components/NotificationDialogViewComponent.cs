using Microsoft.AspNetCore.Mvc;
using ToaPro.Infrastructure;
using ToaPro.Models.ViewModels;

namespace ToaPro.Components
{
    public class NotificationDialogViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title, string body, NotificationDialogType notificationType = NotificationDialogType.Default)
        {
            var dialogModel = new NotificationDialogViewModel { Title = title, Body = body, DialogType = notificationType };
            return View("Default", dialogModel);
        }
    }
}
