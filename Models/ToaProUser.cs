using Microsoft.AspNetCore.Identity;

namespace ToaPro.Models
{
    public class ToaProUser : IdentityUser
    {
        public int UserId { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
