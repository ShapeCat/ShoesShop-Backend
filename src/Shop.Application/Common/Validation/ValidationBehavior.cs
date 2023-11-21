using FluentValidation;
using MediatR;

namespace ShoesShop.Application.Common.Validation
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => this.validators = validators;

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var errors = validators.Select(x => x.Validate(context))
                                   .SelectMany(result => result.Errors)
                                   .Where(error => error is not null)
                                   .ToList();
            if (errors.Any()) throw new ValidationException(errors);
            return next();
        }
    }
}
