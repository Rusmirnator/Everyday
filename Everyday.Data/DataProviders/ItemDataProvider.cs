using Everyday.Core.Models;
using Everyday.Data.Interfaces;

namespace Everyday.Data.DataProviders
{
    public class ItemDataProvider : IItemDataProvider
    {
        #region Fields & Properties
        private readonly IHttpClientService http;
        #endregion

        #region CTOR
        public ItemDataProvider(IHttpClientService http)
        {
            this.http = http;
        }
        #endregion

        #region READ
        public Task<Item?> GetItemByIdAsync(int id)
        {
            return http.Create($"Items/{id}/item").GetCallToObjectAsync<Item>();
        }

        public Task<Item?> GetItemByCodeAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Item>?> GetItemsAsync()
        {
            return await http.Create("Items/items").GetCallToListAsync<Item>();
        }
        #endregion

        #region CREATE
        public async Task<bool> CreateItemAsync(Item newItem)
        {
            HttpResponseMessage? response = await http.Create($"Items/item").PostCallAsync(newItem);

            return response?.IsSuccessStatusCode is true;
        }
        #endregion

        #region UPDATE
        public async Task<bool> UpdateItemAsync(Item updatedItem)
        {
            HttpResponseMessage? response = await http.Create($"Items/item").PutCallAsync(updatedItem);

            return response?.IsSuccessStatusCode is true;
        }
        #endregion

        #region DELETE
        public async Task<bool> DeleteItemAsync(int id)
        {
            HttpResponseMessage? response = await http.Create($"Items/{id}/item").DeleteCallAsync();

            return response?.IsSuccessStatusCode is true;
        }

        public Task<bool> DeleteItemAsync(string code)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
