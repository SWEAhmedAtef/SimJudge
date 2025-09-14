using System.Net;

namespace SimJudge.Application.Common.Exceptions
{
    public class SimJudgeException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorCode { get; }

        public SimJudgeException(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string errorCode = null) 
            : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }

        public SimJudgeException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.InternalServerError, string errorCode = null) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
