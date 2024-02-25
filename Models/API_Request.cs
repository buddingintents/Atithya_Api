namespace Atithya_Api.Models
{
    public class API_Request<T>
    {
        public T? Content { get; set; }
        public string? Configuration { get; set; }
    }
}
