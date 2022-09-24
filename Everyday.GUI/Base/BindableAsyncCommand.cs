using Everyday.GUI.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday.GUI.Base
{
    public class BindableAsyncCommand : BindableCommandBase, IAsyncCommand
    {
        public Task ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class BindableAsyncCommand<T> : BindableCommandBase, IAsyncCommand<T>
    {
        public Task ExecuteAsync(T parameter)
        {
            throw new NotImplementedException();
        }
    }
}
