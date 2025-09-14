using System.Net;

namespace SimJudge.Application.Common.Exceptions
{
    public class ForbiddenException : SimJudgeException
    {
        public ForbiddenException(string message = "Access forbidden") 
            : base(message, HttpStatusCode.Forbidden, "FORBIDDEN")
        {
        }
    }
}
