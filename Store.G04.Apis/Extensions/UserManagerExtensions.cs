using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.G04.Apis.Error;
using Store.G04.Core.Entity.Identity;
using System.Security.Claims;

namespace Store.G04.Apis.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal User)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null)
                return null ;
           var user =await userManager.Users.Include(U => U.address).FirstOrDefaultAsync(U => U.Email == userEmail);
            if (user is null)
                return null;
            return user;

        }
    }
}
