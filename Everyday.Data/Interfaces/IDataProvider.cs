using Everyday.Core.Interfaces;

namespace Everyday.Data.Interfaces
{
    public interface IDataProvider<T> where T : class
    {
        public Task<T?> GetByIdAsync(int id);
        public Task<T?> GetByMemberAsync<TSource, TMember>(Func<TSource, TMember> memberSelector);
        public Task<IEnumerable<T>?> GetAllAsync();
        public Task<IConveyOperationResult> CreateAsync(T newDatum);
        public Task<IConveyOperationResult> UpdateAsync(T updatedDatum);
        public Task<IConveyOperationResult> DeleteByIdAsync(int id);
        public Task<IConveyOperationResult> DeleteByMemberAsync<TSource, TMember>(Func<TSource, TMember> memberSelector);
    }
}
