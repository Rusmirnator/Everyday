﻿using Everyday.Core.Interfaces;

namespace Everyday.Data.Interfaces
{
    public interface IUserDataProvider
    {
        public Task<IConveyOperationResult> GetTokenAsync(string login, string password);
    }
}
