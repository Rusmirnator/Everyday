using Everyday.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everyday.Data.Interfaces
{
    public interface IUserDataProvider
    {
        public Task<IConveyOperationResult> GetTokenAsync(string login, string password);
    }
}
