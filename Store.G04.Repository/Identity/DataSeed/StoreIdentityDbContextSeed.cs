using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Repository.Identity
{
    public static class StoreIdentityDbContextSeed
    {
        public async static Task SeedAppUserAsync(UserManager<AppUser> _UserManager )
        {
            if(_UserManager.Users.Count()==1)
            {
                var User = new AppUser()
                {

                    Email = "adada@gmail.com",
                    DisplayName = "ali2 Amin",
                    UserName = "Ali.Amin2",
                    PhoneNumber = "+201015486320",
                    address = new Address()
                    {
                        Fname = "Ali1",
                        LName = "Amin1",
                        City = "Cairo",
                        Country = "Egypt",
                        Street = "vfdf1vd"

                    }
                };

                var result = await _UserManager.CreateAsync(User, " ");

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"{error.Code}: {error.Description}");
                    }
                }
                else
                {
                    Console.WriteLine("User created successfully.");
                }

            }

        }
    }
}
