using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Everyday.Infrastructure.Common.DataAccess
{
    internal static class HttpExtensions
    {
        internal static HttpRequestMessage PrepareHeaders(this HttpRequestMessage req)
        {
            req.Headers.Accept.Clear();
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return req;
        }

        internal static HttpRequestMessage PrepareContent<T>(this HttpRequestMessage req, T model) where T : class
        {
            JsonSerializerSettings options = new()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };

            req.Content = new StringContent(JsonConvert.SerializeObject(model, options), Encoding.UTF8, "application/json");
            return req;
        }
    }
}
