using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;
using Store.G04.Apis.Extensions;
using Store.G04.Core.Dtos.Auth;
using Store.G04.Core.Entity.Identity;
using Store.G04.Core.Repositories.Contract;
using Store.G04.Service.Services.Tokens;
using System.Security.Claims;

namespace Store.G04.Apis.Controllers
{
 
    public class AccountController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<AppUser> _userManager;
        private readonly AutoMapper.IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AccountController(IUserService userService, 
            Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager,
            IMapper mapper,
            ITokenService tokenService)
        {
            _userService = userService;
          
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> login(LoginDto loginDto)
        {
            var user = await _userService.LoginAsync(loginDto);
            if (user is null)
                return Unauthorized(new ApiErrorResponse(
                    StatusCodes.Status401Unauthorized,
                    "Invalid email or password"
                ));
            return Ok(user);
        
       }

        [Authorize]
        [HttpPost("Address")]
        public async Task<IActionResult> AddAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null)
                return Unauthorized(new ApiErrorResponse(
                    StatusCodes.Status401Unauthorized));

            if (user.address is not null)
                return Conflict(new ApiErrorResponse(
                    StatusCodes.Status409Conflict,
                    "Address already exists"));

            user.address = new Address
            {
                Fname = addressDto.Fname,
                LName = addressDto.LName,
                City = addressDto.City,
                Country = addressDto.Country,
                Street = addressDto.Street
            };

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ApiErrorResponse(StatusCodes.Status500InternalServerError));

            return Ok(_mapper.Map<AddressDto>(user.address));
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

        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status401Unauthorized)]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            var user = await _userManager.FindByEmailAsync(userEmail);

            if (user is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreatTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("Adress")]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null)
                return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

            return Ok(_mapper.Map<AddressDto>(user.address));
        }


        [Authorize]
        [HttpPut("Address")]
        public async Task<IActionResult> UpdateAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindByEmailWithAddressAsync(User);

            if (user is null)
                return Unauthorized(new ApiErrorResponse(
                    StatusCodes.Status401Unauthorized));

            if (user.address is null)
                return NotFound(new ApiErrorResponse(
                    StatusCodes.Status404NotFound,
                    "Address not found"));

            user.address.Fname = addressDto.Fname;
            user.address.LName = addressDto.LName;
            user.address.City = addressDto.City;
            user.address.Country = addressDto.Country;
            user.address.Street = addressDto.Street;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ApiErrorResponse(StatusCodes.Status500InternalServerError));

            return Ok(_mapper.Map<AddressDto>(user.address));
        }

    }
}
