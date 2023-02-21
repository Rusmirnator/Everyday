namespace Everyday.Domain.Interfaces
{
    /// <summary>
    /// Provides methods to consume data from ConsumableAttribute marked members.
    /// </summary>
    public interface IConsumeData
    {
        /// <summary>
        /// Consumes all marked Consumables from given souce.
        /// </summary>
        /// <typeparam name="TFeed"></typeparam>
        /// <typeparam name="TConsumer"></typeparam>
        /// <param name="source">Instance providing data to consume.</param>
        public void Consume<TFeed, TConsumer>(TFeed source) where TConsumer : class;
    }
}
