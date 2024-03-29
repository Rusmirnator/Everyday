﻿using Everyday.Data.Interfaces;
using Everyday.Domain.Interfaces;
using Everyday.Domain.Models;
using Everyday.Services.Interfaces;

namespace Everyday.Services.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private JsonWebToken? AcquiredToken;
        private readonly IUserDataProvider dataProvider;
        private readonly ICryptographyService cryptography;

        private string? CachedLogin { get; set; }
        private string? CachedPassword { get; set; }

        public AuthorizationService(IUserDataProvider dataProvider, ICryptographyService cryptography)
        {
            this.dataProvider = dataProvider;
            this.cryptography = cryptography;
            CachedLogin = string.Empty;
            CachedPassword = string.Empty;
        }

        public async Task<IConveyOperationResult> AcquireCredentialsAsync(string? login = null, string? password = null)
        {
            CachedLogin = cryptography.Encrypt(login!) ?? CachedLogin;
            CachedPassword = cryptography.Encrypt(password!) ?? CachedPassword;

            IConveyOperationResult res = await dataProvider.GetTokenAsync(CachedLogin!, CachedPassword!);

            AcquiredToken = (JsonWebToken?)res.Result;

            return res;
        }

        public JsonWebToken? GetToken()
        {
            return AcquiredToken;
        }

        public string? GetEncodedToken()
        {
            return AcquiredToken?.EncodedToken;
        }
    }
}
