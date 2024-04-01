using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models
{
    public class ToaProUser : IdentityUser
    {
        public int user_id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
