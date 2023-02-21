using Everyday.Data.Interfaces;
using Everyday.Domain.Interfaces;
using Everyday.Domain.Models;
using Everyday.Services.Interfaces;

namespace Everyday.Services.Services
{
    public class ManufacturerService : IManufacturerService
    {
        #region Fields & Properties
        private readonly IDataProvider<Manufacturer> dataProvider;
        #endregion

        #region CTOR
        public ManufacturerService(IDataProvider<Manufacturer> dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        #endregion

        #region READ
        public async Task<IEnumerable<Manufacturer>> GetManufacturersAsync()
        {
            return await dataProvider.GetAllAsync() ?? Enumerable.Empty<Manufacturer>();
        }

        public async Task<Manufacturer?> GetManufacturerByIdAsync(int id)
        {
            return await dataProvider.GetByIdAsync(id);
        }

        public async Task<Manufacturer?> GetManufacturerByNameAsync(string name)
        {
            return await dataProvider.GetByMemberAsync<Manufacturer, string?>(x => x.Name);
        }
        #endregion

        #region CREATE
        public async Task<IConveyOperationResult> CreateManufacturerAsync(Manufacturer newManufacturer)
        {
            return await dataProvider.CreateAsync(newManufacturer);
        }
        #endregion

        #region UPDATE
        public async Task<IConveyOperationResult> UpdateManufacturerAsync(Manufacturer updatedManufacturer)
        {
            return await dataProvider.UpdateAsync(updatedManufacturer);
        }
        #endregion

        #region DELETE
        public async Task<IConveyOperationResult> DeleteManufacturerAsync(string name)
        {
            return await dataProvider.DeleteByMemberAsync<Manufacturer, string?>(x => x.Name);
        }

        public async Task<IConveyOperationResult> DeleteManufacturerAsync(int id)
        {
            return await dataProvider.DeleteByIdAsync(id);
        }
        #endregion
    }
}
