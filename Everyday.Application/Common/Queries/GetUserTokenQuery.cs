using Everyday.Application.Common.Interfaces.Structures;
using Everyday.Application.Common.Models;
using MediatR;

namespace Everyday.Application.Common.Queries
{
    internal record GetUserTokenQuery(LoginRequestModel Login) : IRequest<IOperationResult?>;
}