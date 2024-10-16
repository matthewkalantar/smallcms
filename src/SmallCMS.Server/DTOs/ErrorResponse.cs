namespace SmallCMS.API.DTOs
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }

        public string? Error { get; set; }

        public string? Details { get; set; }

    };
}
