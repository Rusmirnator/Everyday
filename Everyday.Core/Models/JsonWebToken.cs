using Everyday.Domain.Shared;

namespace Everyday.Domain.Models
{
    public class JsonWebToken : DataTransferObject
    {
        public JsonWebToken(string? encodedToken) : base()
        {
            EncodedToken = encodedToken;
        }

        public string? EncodedToken { get; private set; }
    }
}
