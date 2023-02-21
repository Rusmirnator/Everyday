namespace Everyday.Domain.Interfaces
{
    /// <summary>
    /// Provides methods to feed data to ConsumableAttribute members.
    /// </summary>
    public interface IFeedData
    {
        /// <summary>
        /// Feeds all target's marked Consumables with data from host instance.
        /// </summary>
        /// <typeparam name="TFeed"></typeparam>
        /// <typeparam name="TConsumer"></typeparam>
        /// <param name="target">Instance to be fed with data.</param>
        public void Feed<TFeed, TConsumer>(TFeed target);
    }
}
