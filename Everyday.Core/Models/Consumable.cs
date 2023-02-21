using Everyday.Domain.Shared;

namespace Everyday.Domain.Models
{
    public class Consumable : DataTransferObject
    {
        #region Fields & Properties
        public int Id { get; set; }
        public double? Protein { get; set; }
        public double? Carbohydrates { get; set; }
        public double? Sugars { get; set; }
        public double? Fat { get; set; }
        public double? SaturatedFat { get; set; }
        public double? Fiber { get; set; }
        public double? Salt { get; set; }
        public double? Energy { get; set; }
        public int? ItemId { get; set; }
        #endregion
    }
}
