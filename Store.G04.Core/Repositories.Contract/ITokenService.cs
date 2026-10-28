using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Entity.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Repositories.Contract
{

    public interface ITokenService
    {



        Task<string> CreatTokenAsync(AppUser user , UserManager<AppUser> userManager);
    }
}
