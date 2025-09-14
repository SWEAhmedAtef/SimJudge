using System.Net;

namespace SimJudge.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occurred.", new Exception())
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(Dictionary<string, string[]> errors) : this()
        {
            Errors = errors;
        }

        public ValidationException(string property, string message) : this()
        {
            Errors.Add(property, new[] { message });
        }
    }
}
