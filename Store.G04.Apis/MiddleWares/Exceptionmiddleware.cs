using Store.G04.Apis.Error;

namespace Store.G04.Apis.MiddleWares 
{
    public class Exceptionmiddleware //Without exception middleware, an error might reach the default error-handling mechanism in ASP.NET Core.
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<Exceptionmiddleware> _logger;
        private readonly IHostEnvironment _env;

        public Exceptionmiddleware( RequestDelegate next , ILogger<Exceptionmiddleware> logger ,IHostEnvironment env )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext Context)
        {

            try
            {
                await _next.Invoke(Context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                Context.Response.ContentType = "application/json";
                Context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                var response = _env.IsDevelopment() ?
                       new ApiExceptionsResponse
                        (StatusCodes.Status500InternalServerError, ex?.Message, ex?.StackTrace?.ToString())
                        :
                        new ApiExceptionsResponse
                        (StatusCodes.Status500InternalServerError, null, "Internal Server Error");


                var json = System.Text.Json.JsonSerializer.Serialize(response);


                await Context.Response.WriteAsync(json);
            }
        }
    }
}
