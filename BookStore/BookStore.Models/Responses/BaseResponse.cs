using System.Net;

namespace BookStore.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; init; }
        public string Message { get; set; }
    }
}
