namespace Everyday.GUI.Base
{
    public class DependencyInjectoinSource : IMarkupExtension
    {
        public static Func<Type, object> Resolver { get; set; }
        public Type Type { get; set; }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return Resolver?.Invoke(Type);
        }
    }
}
