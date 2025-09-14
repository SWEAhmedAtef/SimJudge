using System.Net;

namespace SimJudge.Application.Common.Exceptions
{
    public class NotFoundException : SimJudgeException
    {
        public NotFoundException(string message) : base(message, HttpStatusCode.NotFound, "NOT_FOUND")
        {
        }

        public NotFoundException(string entityName, object key) 
            : base($"{entityName} with key '{key}' was not found.", HttpStatusCode.NotFound, "NOT_FOUND")
        {
        }
    }
}
