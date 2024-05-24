using Microsoft.AspNetCore.Mvc;
using ToaPro.Models.ViewModels;

namespace ToaPro.Components
{
    public class FileUploadViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string title)
        {
            var viewModel = new FileUploadViewModel
            {
                ButtonText = title,
                UserRole = Infrastructure.UserRole.TA
            };
            return View("Default", viewModel);
        }
    }
}
