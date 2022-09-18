using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Everyday.GUI.Base.Interfaces
{
    public interface IAsyncCommand : ICommand
    {
        public Task ExecuteAsync();
    }

    public interface IAsyncCommand<T> : ICommand
    {
        public Task ExecuteAsync(T parameter);
    }
}
