using Store.G04.Core.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Core.Repositories.Contract
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto register);
        Task<bool> CheckEmailExitsAsync(string email);
    }
}
