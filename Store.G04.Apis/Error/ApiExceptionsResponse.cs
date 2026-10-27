namespace Store.G04.Apis.Error
{
    public class ApiExceptionsResponse : ApiErrorResponse
    {
        public string? Details { get; set; }
        public ApiExceptionsResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
