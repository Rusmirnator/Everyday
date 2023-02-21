using Everyday.Application.Common.Interfaces.Services;
using Everyday.Application.Common.Interfaces.Structures;
using Everyday.Application.Common.Models;
using Everyday.Application.Common.Queries;
using MediatR;

namespace Everyday.Application.Common.Handlers
{
    internal class GetUserTokenHandler : IRequestHandler<GetUserTokenQuery, IOperationResult?>
    {
        private readonly IIdentityService identityService;

        public GetUserTokenHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<IOperationResult?> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return default;
            }

            var res = await identityService.LoginAsync(request.Login);

            return new OperationResultModel(string.IsNullOrEmpty(res.EncodedToken), string.Empty);
        }
    }
}
