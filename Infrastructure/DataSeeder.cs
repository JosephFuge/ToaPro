using Microsoft.AspNetCore.Identity;
using System.Net.WebSockets;
using ToaPro.Controllers;
using ToaPro.Models;

namespace ToaPro.Infrastructure
{
    public class DataSeeder
    {
        public ToaProContext _context { get; set; }
        public UserManager<ToaProUser> _userManager { get; set; }

        public DataSeeder(ToaProContext tempContext, UserManager<ToaProUser> userManager)
        {
            _context = tempContext;
            _userManager = userManager;
        }

        public async Task<bool> SeedIndividualUser(ToaProUser user, string password, string role)
        {
            ToaProUser? coord = await _userManager.FindByEmailAsync(user.Email);

            if (coord == null)
            {
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if (roleResult.Succeeded)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
