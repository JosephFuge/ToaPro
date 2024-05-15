using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models.ViewModels
{
    public class UsersViewModel
    {
        [Required]
        public IDictionary<string, IList<ToaProUser>> RoleUsers { get; set; } = new Dictionary<string, IList<ToaProUser>>();

        [Required]
        public IList<Student> Students { get; set; } = new List<Student>();
    }
}
