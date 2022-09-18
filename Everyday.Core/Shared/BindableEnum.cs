namespace Everyday.Core.Shared
{
    public class BindableEnum
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public BindableEnum(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}
