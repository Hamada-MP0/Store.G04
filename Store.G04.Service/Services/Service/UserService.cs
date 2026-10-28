using Microsoft.AspNetCore.Identity;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Entity.Identity;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Service.Services.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.G04.Service.Services.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

   

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var User = await _userManager.FindByEmailAsync(loginDto.Email);
            if (User is null) return null;

            var resultUser = await _signInManager.CheckPasswordSignInAsync(User, loginDto.Password, false);
            if (!resultUser.Succeeded) return null;
            return new UserDto()
            {
                DisplayName= User.DisplayName, 
                Email=User.Email,
                Token =await _tokenService.CreatTokenAsync(User, _userManager)

       
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto register)
        {
            //  Check if email already exists
            if (await CheckEmailExitsAsync(register.Email))
                return null;

            //  Create AppUser
            var user = new AppUser
            {
                DisplayName = register.DisplayName,
                Email = register.Email,
                UserName = register.Email.Split("@")[0],
                PhoneNumber = register.PhoneNumber
            };

            //  Create user and hash password
            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
                return null;

            // Generate token
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreatTokenAsync(user, _userManager)
            };
        }

        public async Task<bool> CheckEmailExitsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

    }
}
