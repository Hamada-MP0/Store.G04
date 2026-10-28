using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Repositories.Contract;

namespace Store.G04.Apis.Controllers
{
 
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);
            if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            return Ok(user);
        
       }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _userService.RegisterAsync(registerDto);

            if (user is null)
                return BadRequest(
                    new ApiErrorResponse(StatusCodes.Status400BadRequest,"Invalid Registration"));

            return Ok(user);
        }
    }
}
