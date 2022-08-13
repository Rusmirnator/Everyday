namespace Everyday.Core.Models
{
    public class ItemDefinition
    {
        #region Fields & Properties
        public int Id { get; set; }
        public int DimensionsMeasureUnitId { get; set; }
        public int WeightMeasureUnitId { get; set; }
        public int ItemCategoryTypeId { get; set; }
        public int? ContainerId { get; set; }
        #endregion
    }
}
