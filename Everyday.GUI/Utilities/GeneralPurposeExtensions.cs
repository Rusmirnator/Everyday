using Everyday.Core.Shared;

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

        /// <summary>
        /// Safely executes task in FireAndForget manner - allows to move with code execution immediately after executing.
        /// </summary>
        /// <param name="task">Task to be safely executed.</param>
        /// <param name="continueOnCapturedContext">Task's awaiter configuration.</param>
        /// <param name="onException">A delegate to be executed when task throws an exception</param>
        public static async void FireAndForget(this Task task, bool continueOnCapturedContext = true, Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception ex) when (onException != null)
            {
                onException?.Invoke(ex);
            }
        }
    }
}
