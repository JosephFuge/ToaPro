using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models
{
    public class ToaProUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
