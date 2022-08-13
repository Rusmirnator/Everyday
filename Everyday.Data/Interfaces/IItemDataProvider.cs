using Everyday.Core.Models;

namespace Everyday.Data.Interfaces
{
    public interface IItemDataProvider
    {
        public Task<Item?> GetItemByIdAsync(int id);
        public Task<Item?> GetItemByCodeAsync(string code);
        public Task<IEnumerable<Item>?> GetItemsAsync();
        public Task<bool> CreateItemAsync(Item newItem);
        public Task<bool> UpdateItemAsync(Item updatedItem);
        public Task<bool> DeleteItemAsync(int id);
        public Task<bool> DeleteItemAsync(string code);
    }
}
