using System.Runtime.CompilerServices;

namespace Everyday.Domain.Attributes
{
    /// <summary>
    /// Provides mapping information for IConsumeData and IFeedData.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class ConsumableAttribute : Attribute
    {
        #region Fields & Properties
        private readonly Type consumerType;
        private readonly string? consumerName;
        private readonly string? path;
        #endregion

        #region CTOR
        /// <summary>
        /// Exposes field or property for consumption.
        /// </summary>
        /// <param name="consumerType">Consumer type.</param>
        /// <param name="consumerName">Name of consumer instance - if left empty, marked property or field becomes available for consumption for all type compliant consumers.</param>
        /// <param name="path">Consumer's member name.</param>
        public ConsumableAttribute(Type consumerType, string? consumerName = null, [CallerMemberName] string? path = null)
        {
            this.consumerType = consumerType;
            this.consumerName = consumerName;
            this.path = path;
        }
        #endregion

        #region Public API
        public bool IsMatched(string? consumerName)
        {
            if (string.IsNullOrEmpty(this.consumerName))
            {
                return true;
            }

            return !string.IsNullOrEmpty(this.consumerName) && this.consumerName.Equals(consumerName ?? string.Empty, StringComparison.Ordinal);
        }

        public bool IsTypeCompliant(Type consumerType)
        {
            return this.consumerType is not null && consumerType is not null && this.consumerType.Equals(consumerType);
        }

        public string? GetPath()
        {
            return path;
        }
        #endregion
    }
}
