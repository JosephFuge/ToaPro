using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models.ViewModels
{
    public class UsersViewModel
    {
        [Required]
        public IDictionary<ToaProUser, IList<string>> UserRoles { get; set; } = new Dictionary<ToaProUser, IList<string>>();

        [Required]
        public IList<Student> Students { get; set; } = new List<Student>();
    }
}
