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
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Items/item").PostCallAsync(newItem);

            if (response is not null)
            {
                res = await response!.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateItemAsync(Item updatedItem)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Items/item").PutCallAsync(updatedItem);

            if (response is not null)
            {
                res = await response.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteItemAsync(int id)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Items/{id}/item").DeleteCallAsync();

            if (response is not null)
            {
                res = await response.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }

        public async Task<IConveyOperationResult> DeleteItemAsync(string code)
        {
            IConveyOperationResult? res = null;

            HttpResponseMessage? response = await http.Create($"Items/item?code={code}").DeleteCallAsync();

            if (response is not null)
            {
                res = await response.DeserializeContent<OperationResult>();
            }

            return res ?? new OperationResult();
        }
        #endregion
    }
}
