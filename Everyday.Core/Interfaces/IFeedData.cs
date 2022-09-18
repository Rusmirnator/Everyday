using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday.Core.Interfaces
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
