using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;
using Store.G04.Repository.Data.Context;

namespace Store.G04.Apis.Controllers
{

    //: ASP.NET Core بيستبدلها باسم الـ Controller من غير كلمة Controller، يعني هنا Buggy
    
    public class BuggyController : BaseApiController
    {
        private readonly StoreDbContext _context;
    
        public BuggyController(StoreDbContext context)
        {
            _context = context;
        }

        [HttpGet("notfound")] // api/buggy/notfound
        public async Task<IActionResult> GetNotFoundresault()
        {
            var brand = await _context.Brands.FindAsync(100);

            if (brand == null) return NotFound (new ApiErrorResponse(404));

            return Ok(brand);

        }


        [HttpGet("servererror")] // api/buggy/servererror
        public async Task<IActionResult> GetServerError()
        {
            var brand = await _context.Brands.FindAsync(100);

            var brandToStringg = brand.ToString(); // This will throw a NullReferenceException if brand is null

            return Ok(brand);


        }

        [HttpGet("badrequest")] // api/buggy/badrequest
        public async Task<IActionResult> GetBadRequest()
        {
             return BadRequest(new ApiErrorResponse(400));


        }


        [HttpGet("badrequest/{id}")] // api/buggy/badrequest
        public async Task<IActionResult> GetBadRequest(int id) //validation error
        { // I am not responsible for validating the id parameter, the framework will do that for me,
          // if the id is not valid, it will return a 400 Bad Request response automatically
          //the model state is automatically validated by the framework, 
          // there is services is built in  is deals with validation errors and returns a 400 Bad Request response with the validation errors in the response body
          // the solution .. go to the program.cs file and configure the ApiBehaviorOptions to return a custom response instead of the default 400 Bad Request response




            return Ok(id); 

        }


        [HttpGet("unAuthorized")] // api/buggy/unAuthorized
        public async Task<IActionResult> GetUnAuthorized()
        {

            return Unauthorized(new ApiErrorResponse(401));

        }


    }
}
