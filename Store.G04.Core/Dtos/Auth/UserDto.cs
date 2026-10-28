using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Dtos.Auth
{
    // Format the Response  from loginDto
    public class UserDto
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Token { get; set; }
         
    }
}
