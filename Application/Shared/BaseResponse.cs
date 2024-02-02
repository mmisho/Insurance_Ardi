#nullable disable

using System.Net;

namespace Application.Shared
{
    public class BaseResponse
    {
        public BaseResponse(string message, HttpStatusCode httpStatusCode)
        {
            HttpStatusCode = httpStatusCode;
            Message = message;
        }

        public HttpStatusCode HttpStatusCode { get; private set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
    }
}
