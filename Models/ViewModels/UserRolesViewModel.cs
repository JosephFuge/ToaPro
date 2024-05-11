using Microsoft.AspNetCore.Identity;
using ToaPro.Infrastructure;

namespace ToaPro.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public UserRole SelectedRole { get; set; } = UserRole.None;
        public IDictionary<ToaProUser, IList<string>> UserRoles { get; set; }
        public IDictionary<Student, IList<string>>? StudentRoles { get; set; } = null;
    }
}
