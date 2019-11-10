using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using MediatrSampleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MediatrSampleApi.Handlers.Query
{
    /// <summary>
    /// Gets Email to find if customer exists
    /// </summary>
    public class DoesCustomerExistsRequest : IRequest<bool>
    {
        /// <summary>
        /// customer email
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// </summary>
    public class DoesCustomerExistsRequestValidator : AbstractValidator<DoesCustomerExistsRequest>
    {
        /// <summary>
        /// </summary>
        public DoesCustomerExistsRequestValidator()
        {
            RuleFor(x => x.Email).MaximumLength(120);
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter valid customer email");
        }
    }

    /// <summary>
    /// handler for checking if customer exists
    /// </summary>
    public class DoesCustomerExistsHandler : IRequestHandler<DoesCustomerExistsRequest, bool>
    {
        private ApiDbContext context;

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        public DoesCustomerExistsHandler(ApiDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// checks if customer exists
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> Handle(DoesCustomerExistsRequest request, CancellationToken cancellationToken)
        {
            return await context.Customers.AnyAsync(x => x.Email == request.Email, cancellationToken);
        }
    }
}
