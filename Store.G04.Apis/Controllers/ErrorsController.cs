using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G04.Apis.Error;

namespace Store.G04.Apis.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // to hide this controller from swagger
    public class ErrorsController : ControllerBase
    { // if any request to endpoint that does not exist, it will be redirected to this controller and return a 404 error


        public IActionResult Error(int code)
        {
            return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, "Page not found."));
        }
    }
}
