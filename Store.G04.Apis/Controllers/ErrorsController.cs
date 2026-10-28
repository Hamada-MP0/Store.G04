using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;

namespace Store.G04.Apis.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Error(int code)
        {
            var message = code switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Page not found.",
                405 => "Method not allowed.",
                415 => "Unsupported media type.",
                429 => "Too many requests.",
                _ => "An error occurred."
            };

            return StatusCode(
                code,
                new ApiErrorResponse(code, message));
        }
    }
}