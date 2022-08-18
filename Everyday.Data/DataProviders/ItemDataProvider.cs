using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.Core.Shared;
using Everyday.Data.Interfaces;
using Everyday.Data.Utilities;

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
            return http.Create($"Items/item?code={code}").GetCallToObjectAsync<Item>();
        }

        public async Task<IEnumerable<Item>?> GetItemsAsync()
        {
            return await http.Create("Items/items").GetCallToListAsync<Item>();
        }
        #endregion

        #region CREATE
        public async Task<IConveyOperationResult> CreateItemAsync(Item newItem)
        {
            IConveyOperationResult? res;

            HttpResponseMessage response = await http.Create($"Items/item").PostCallAsync(newItem);

            res = await response!.DeserializeContent<IConveyOperationResult>();

            return res ?? new OperationResult(response);
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateItemAsync(Item updatedItem)
        {
            IConveyOperationResult? res;

            HttpResponseMessage? response = await http.Create($"Items/item").PutCallAsync(updatedItem);

            res = await response.DeserializeContent<IConveyOperationResult>();

            return res ?? new OperationResult(response);
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteItemAsync(int id)
        {
            IConveyOperationResult? res;

            HttpResponseMessage? response = await http.Create($"Items/{id}/item").DeleteCallAsync();

            res = await response.DeserializeContent<IConveyOperationResult>();

            return res ?? new OperationResult(response);
        }

        public Task<IConveyOperationResult> DeleteItemAsync(string code)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
