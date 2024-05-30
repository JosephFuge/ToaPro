using ToaPro.Infrastructure;

namespace ToaPro.Models.ViewModels
{
    public class NotificationDialogViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public NotificationDialogType DialogType { get; set; } = NotificationDialogType.Default;
    }
}
