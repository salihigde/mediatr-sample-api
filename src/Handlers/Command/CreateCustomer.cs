using System.Threading;
using System.Threading.Tasks;
using MediatrSampleApi.Handlers.Contracts;
using MediatrSampleApi.Models;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace MediatrSampleApi.Handlers.Command
{
    /// <summary>
    /// </summary>
    public class CustomerRequest : IRequest<CustomerCreateResponse>
    {
        /// <summary>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        public string Email { get; set; }
    }

    /// <summary>
    /// validation rules for creating customer
    /// </summary>
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        /// <summary>
        /// </summary>
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Name).MaximumLength(80);
            RuleFor(x => x.Email).MaximumLength(120);
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter valid customer email");
        }
    }

    /// <summary>
    /// Create customer handler
    /// </summary>
    public class CreateCustomer : IRequestHandler<CustomerRequest, CustomerCreateResponse>
    {
        private readonly ApiDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public CreateCustomer(ApiDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates customer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>returns customer id of the new customer</returns>
        public async Task<CustomerCreateResponse> Handle(CustomerRequest request, CancellationToken cancellationToken)
        {
            var customer = mapper.Map<CustomerRequest, Customer>(request);

            context.Customers.Add(customer);

            await context.SaveChangesAsync();

            return new CustomerCreateResponse { Id = customer.Id };
        }
    }
}
