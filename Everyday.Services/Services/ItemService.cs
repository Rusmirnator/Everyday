using Everyday.Core.Interfaces;
using Everyday.Core.Models;
using Everyday.Data.Interfaces;
using Everyday.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Task<IConveyOperationResult> CreateItemAsync(Item newItem)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region UPDATE
        public Task<IConveyOperationResult> UpdateItemAsync(Item updatedItem)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region DELETE
        public Task<IConveyOperationResult> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IConveyOperationResult> DeleteItemAsync(string code)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
