namespace Everyday.Application.Common.Interfaces.Structures
{
    public interface IOptions<T> where T : class
    {
        public T? Configuration { get; set; }
    }
}
