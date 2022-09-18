using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AsyncCommandAttribute : Attribute
    {
    }
}
