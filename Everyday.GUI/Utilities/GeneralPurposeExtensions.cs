using Everyday.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday.GUI.Utilities
{
    public static class GeneralPurposeExtensions
    {
        /// <summary>
        /// Returns bindable representation of given Enum key and value pairs.
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static IEnumerable<BindableEnum> ToEnumerable(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                return Enumerable.Empty<BindableEnum>();
            }

            return Enum.GetValues(enumType)
                            .Cast<int>()
                                .Select(e => new BindableEnum(Enum.GetName(enumType, e), e));
        }

        /// <summary>
        /// Returns actual value from BindableEnum representation instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="representation"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ToEnum<T>(this BindableEnum representation) where T : Enum
        {
            if (representation is null)
            {
                throw new ArgumentNullException(nameof(representation));
            }
            return (T)Enum.Parse(typeof(T), representation.Name);
        }
    }
}
