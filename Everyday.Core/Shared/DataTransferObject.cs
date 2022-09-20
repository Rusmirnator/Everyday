using Everyday.Core.Attributes;
using Everyday.Core.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Everyday.Core.Shared
{
    public class DataTransferObject : IConveyOperationResult, IConsumeData, IFeedData
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Result { get; set; }

        public DataTransferObject()
        {
            Result = this;
        }

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
            foreach (PropertyInfo property in typeof(TConsumer).GetProperties())
            {
                System.Diagnostics.Debug.WriteLine(property.Name);
            }
        }
    }
}
