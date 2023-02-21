using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Everyday.Domain.Interfaces
{
    /// <summary>
    /// Provides controlled-lifetime access do class properties.
    /// </summary>
    public interface IPropertyBag
    {
        [JsonIgnore]
        public ConcurrentDictionary<string, PropertyInfo> Bag { get; }

        public IDictionary<string, PropertyInfo> Collect();
    }
}
