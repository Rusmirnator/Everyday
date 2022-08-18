using Everyday.Core.Interfaces;
using Everyday.Core.Shared;

namespace Everyday.Core.Models
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
