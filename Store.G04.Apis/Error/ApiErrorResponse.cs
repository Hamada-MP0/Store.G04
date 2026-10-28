namespace Store.G04.Apis.Error
{
    public class ApiErrorResponse
    {
        public int statusCode { get; set; }
        public string? message { get; set; }

        public ApiErrorResponse(int statusCode, string? message=null)
        {
            //statusCode = statusCode;
            this.statusCode = statusCode;
            this.message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        private string? GetDefaultMessageForStatusCode(int statusCode)
        {
          var defaultMessage = statusCode switch
          {
              400 => "Bad Request",
              401 => "Unauthorized",
              403 => "Forbidden",
              404 => "Resource Not Found",
              405 => "Method Not Allowed",
              408 => "Request Timeout",
              409 => "Conflict",
              415 => "Unsupported Media Type",
              422 => "Unprocessable Entity",
              429 => "Too Many Requests",
              500 => "Internal Server Error",
              502 => "Bad Gateway",
              503 => "Service Unavailable",
              504 => "Gateway Timeout",
              _ => "An error occurred"
          };
            return defaultMessage;
        }

    }
}
