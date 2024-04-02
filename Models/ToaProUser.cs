using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models
{
    public class ToaProUser : IdentityUser
    {
        // Hidden in the IdentityUser class
        // public string Id { get; set; }
        // public string Email { get; set; }
        // public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
