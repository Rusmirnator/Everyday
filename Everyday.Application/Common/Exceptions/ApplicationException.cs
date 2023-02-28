using System.Globalization;

namespace Everyday.Application.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException() : base() { }
        public ApplicationException(string message) : base(message) { }
        public ApplicationException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
