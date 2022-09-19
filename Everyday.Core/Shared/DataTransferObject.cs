using Everyday.Core.Attributes;
using Everyday.Core.Interfaces;
using System.Reflection;

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
            foreach (PropertyInfo property in typeof(TFeed)
                                                .GetProperties()
                                                    .Where(p => p.GetCustomAttribute<ConsumableAttribute>() is not null))
            {
                typeof(TConsumer)
                    .GetProperty(property.Name)!
                        .SetValue(this, property.GetValue(source));
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
