using MediatR;
using Serilog;
using ShoesShop.Application.Common.Interfaces;

namespace ShoesShop.Application.Common.Logging
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : IRequest<TResponse>
    {

        private ICurrentUserService currentUserService;

        public LoggingBehavior(ICurrentUserService currentUserService) => this.currentUserService = currentUserService;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = currentUserService.UserId;
            Log.Information("Request: {requestName}({@request}) by user {@userId}", requestName, request, userId);

            return await next();
        }
    }
}
