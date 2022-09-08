namespace Everyday.Data.Interfaces
{
    public interface IHttpClientService
    {
        public void AuthorizeBearer(string accessToken);
        public IHttpClientService Create(string endpoint);
        public Task<HttpResponseMessage> GetCallAsync();
        public Task<HttpResponseMessage?> PostCallAsync<T>(T model) where T : class;
        public Task<HttpResponseMessage?> PostCallAsync();
        public Task<HttpResponseMessage?> PutCallAsync<T>(T model) where T : class;
        public Task<HttpResponseMessage?> PutCallAsync();
        public Task<HttpResponseMessage?> DeleteCallAsync();
        public Task<List<T>?> GetCallToListAsync<T>();
        public Task<T?> GetCallToObjectAsync<T>();
    }
}
