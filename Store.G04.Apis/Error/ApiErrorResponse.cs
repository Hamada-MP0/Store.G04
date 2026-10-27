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
              404 => "Resource Is Not Found",
              500 => "Internal Server Error",
              _ => null
          };
            return defaultMessage;
        }

    }
}
