using Everyday.Domain.Attributes;
using Everyday.Domain.Interfaces;
using System.Reflection;

namespace Everyday.Domain.Shared
{
    public class DataTransferObject : IConsumeData, IFeedData
    {
        #region CTOR
        public DataTransferObject()
        {

        }
        #endregion

        #region Public API
        public void Consume<TFeed, TConsumer>(TFeed source) where TConsumer : class
        {
            var consumables = typeof(TFeed)
                                .GetProperties()
                                    .Where(p => p.GetCustomAttribute<ConsumableAttribute>() is not null)
                                        .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<ConsumableAttribute>() })
                                            .Where(c => c.Attribute!.IsTypeCompliant(typeof(TConsumer)))
                                                .ToDictionary(c => c.Attribute!.GetPath() ?? c.Property.Name);

            foreach (PropertyInfo property in typeof(TConsumer).GetProperties())
            {
                if (consumables.TryGetValue(property.Name, out var consumable))
                {
                    property.SetValue(this, consumable.Property.GetValue(source), null);
                }
            }
        }

        public void Feed<TFeed, TConsumer>(TFeed target)
        {
            var consumables = typeof(TFeed)
                                .GetProperties()
                                    .Where(p => p.GetCustomAttribute<ConsumableAttribute>() is not null)
                                        .Select(p => new { Property = p, Attribute = p.GetCustomAttribute<ConsumableAttribute>() })
                                            .Where(c => c.Attribute!.IsTypeCompliant(typeof(TConsumer)))
                                                .ToDictionary(c => c.Attribute!.GetPath() ?? c.Property.Name);

            foreach (PropertyInfo property in typeof(TConsumer).GetProperties())
            {
                if (consumables.TryGetValue(property.Name, out var consumable))
                {
                    consumable.Property.SetValue(target, property.GetValue(this, null));
                }
            }
        }
        #endregion
    }
}
