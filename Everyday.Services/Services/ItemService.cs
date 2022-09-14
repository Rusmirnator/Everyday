using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.Data.Interfaces;
using Everyday.Services.Interfaces;

namespace Everyday.Services.Services
{
    public class ItemService : IItemService
    {
        #region Fields & properties
        private readonly IItemDataProvider dataProvider;
        #endregion

        #region CTOR
        public ItemService(IItemDataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        #endregion

        #region READ
        public async Task<Item?> GetItemByCodeAsync(string code)
        {
            return await dataProvider.GetItemByCodeAsync(code);
        }

        public async Task<Item?> GetItemByIdAsync(int id)
        {
            return await dataProvider.GetItemByIdAsync(id);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await dataProvider.GetItemsAsync() ?? Enumerable.Empty<Item>();
        }
        #endregion

        #region CREATE
        public async Task<IConveyOperationResult> CreateItemAsync(Item newItem)
        {
            return await dataProvider.CreateItemAsync(newItem);
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateItemAsync(Item updatedItem)
        {
            return await dataProvider.UpdateItemAsync(updatedItem);
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteItemAsync(int id)
        {
            return await dataProvider.DeleteItemAsync(id);
        }

        public async Task<IConveyOperationResult> DeleteItemAsync(string code)
        {
            return await dataProvider.DeleteItemAsync(code);
        }
        #endregion
    }
}
