using System.ComponentModel.DataAnnotations;
using ToaPro.Infrastructure;

namespace ToaPro.Models.ViewModels
{
    public class FileUploadViewModel
    {
        [Required]
        public IFormFile CsvFile { get; set; }
        [Required]
        public UserRole UserRole { get; set; }
        public string ButtonText { get; set; } = "Upload";
    }
}
