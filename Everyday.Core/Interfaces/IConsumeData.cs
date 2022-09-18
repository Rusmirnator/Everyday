using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday.Core.Interfaces
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
        public void Consume<TFeed, TConsumer>(TFeed source);
    }
}
