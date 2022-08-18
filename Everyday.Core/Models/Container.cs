using Everyday.Core.Shared;

namespace Everyday.Core.Models
{
    public class Container : DataTransferObject
    {
        #region Fields & Properties
        public int Id { get; set; }
        public int TrashTypeId { get; set; }
        public bool IsReusable { get; set; }
        #endregion
    }
}
