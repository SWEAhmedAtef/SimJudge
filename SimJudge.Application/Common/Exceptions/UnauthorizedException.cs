using System.Net;

namespace SimJudge.Application.Common.Exceptions
{
    public class UnauthorizedException : SimJudgeException
    {
        public UnauthorizedException(string message = "Unauthorized access") 
            : base(message, HttpStatusCode.Unauthorized, "UNAUTHORIZED")
        {
        }
    }
}
