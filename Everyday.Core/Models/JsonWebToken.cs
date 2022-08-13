using Everyday.Core.Interfaces;

namespace Everyday.Core.Models
{
    public class JsonWebToken : IConveyOperationResult
    {
        public JsonWebToken(string? encodedToken)
        {
            EncodedToken = encodedToken;
            Result = this;
        }

        public string? EncodedToken { get; private set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }
    }
}
