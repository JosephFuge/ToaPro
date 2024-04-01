using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ToaPro.Models
{
    public class ToaProUser : IdentityUser
    {
        [Key]
        public int user_id { get; set; }
        public int identity_id { get; set; }
        public string user_type { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
