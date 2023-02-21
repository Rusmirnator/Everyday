namespace Everyday.Infrastructure.Common.DataAccess
{
    public class HttpClientConfiguration
    {
        public TimeSpan Timeout { get; set; }
        public Uri? BaseAddress { get; set; }
    }
}
