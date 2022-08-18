namespace Everyday.GUI.Base
{
    public class ViewModelParameter
    {
        #region Fields & Properties
        private readonly object value;
        private readonly string compositeKey;

        public string Name { get; }
        public string Audience { get; }
        public Type Type { get; }
        #endregion

        #region CTOR
        public ViewModelParameter(string audience, string name, object value, Type type)
        {
            Audience = audience;
            Name = name;
            this.value = value;
            Type = type;
            compositeKey = string.Concat(audience, '.', name);
        }
        #endregion

        #region Public API
        public T GetValue<T>()
        {
            try
            {
                return (T)Convert.ChangeType(value, Type);
            }
            catch (InvalidCastException)
            {
                return default;
            }
        }

        public string GetCompositeKey()
        {
            return compositeKey;
        }
        #endregion
    }
}
