using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Entity.Identity
{
    public class AppUser :IdentityUser // from package Identity
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}
