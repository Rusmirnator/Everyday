using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace Everyday.Data.Utilities
{
    public static class HTTPClientExtensions
    {
        /// <summary>
        /// Wyciąga z HttpResponseMessage zawartość tablic [ ]. Każdy wpis w tablicy oddziela nową linią.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static async Task<string> ReadResponseMessageAsync(this HttpResponseMessage msg)
        {
            StringBuilder sb = new StringBuilder();
            bool readingMessage = false;

            string message = await msg.Content.ReadAsStringAsync();

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i].Equals('"'))
                {
                    continue;
                }

                if (message[i].Equals(']'))
                {
                    readingMessage = false;
                    _ = sb.Append(Environment.NewLine);
                }

                if (readingMessage)
                {
                    _ = sb.Append(message[i]);
                }

                if (message[i].Equals('['))
                {
                    readingMessage = true;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Wyciąga pełną zawartość (Content) z HttpResponseMessage bez obróbki.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static async Task<string> ReadResponseContentAsync(this HttpResponseMessage msg)
        {
            StringBuilder sb = new StringBuilder();

            string message = await msg.Content.ReadAsStringAsync();

            for (int i = 0; i < message.Length; i++)
            {
                if (message[i].Equals('"'))
                {
                    continue;
                }

                _ = sb.Append(message[i]);
            }
            return sb.ToString();
        }

        public static HttpRequestMessage PrepareHeaders(this HttpRequestMessage req)
        {
            req.Headers.Accept.Clear();
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return req;
        }

        public static HttpRequestMessage PrepareContent<T>(this HttpRequestMessage req, T model) where T : class
        {
            req.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            return req;
        }

        public static string ToQueryString<T>(this T obj) where T : class
        {
            if (obj is null)
            {
                return "";
            }

            StringBuilder query = new("?");

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                _ = query.Append($"{prop.Name}={prop?.GetGetMethod()?.Invoke(obj, null)}&");
            }
            _ = query.Remove(query.Length - 1, 1);
            return query.ToString();
        }
    }
}
