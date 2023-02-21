using Everyday.Domain.Interfaces;
using Everyday.Domain.Models;

namespace Everyday.Data.Interfaces
{
    public interface IItemDataProvider
    {
        public Task<Item?> GetItemByIdAsync(int id);
        public Task<Item?> GetItemByCodeAsync(string code);
        public Task<IEnumerable<Item>?> GetItemsAsync();
        public Task<IConveyOperationResult> CreateItemAsync(Item newItem);
        public Task<IConveyOperationResult> UpdateItemAsync(Item updatedItem);
        public Task<IConveyOperationResult> DeleteItemAsync(int id);
        public Task<IConveyOperationResult> DeleteItemAsync(string code);
    }
}
