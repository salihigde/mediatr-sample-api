using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatrSampleApi.Handlers.Contracts;
using MediatrSampleApi.Models;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MediatrSampleApi.Handlers.Query
{
    /// <summary>
    /// </summary>
    public class CustomerWithOrdersRequest : IRequest<CustomerWithOrdersResponse>
    {
        /// <summary>
        /// </summary>
        public Guid CustomerId { get; set; }
    }

    /// <summary>
    /// </summary>
    public class CustomerWithOrdersRequestValidator : AbstractValidator<CustomerWithOrdersRequest>
    {
        /// <summary>
        /// </summary>
        public CustomerWithOrdersRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
        }
    }

    /// <summary>
    /// get a customer with orders handler
    /// </summary>
    public class GetCustomerWithOrders : IRequestHandler<CustomerWithOrdersRequest, CustomerWithOrdersResponse>
    {
        private readonly ApiDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public GetCustomerWithOrders(ApiDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// retrieves one customer with orders by given customer id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>customer with list of orders</returns>
        public async Task<CustomerWithOrdersResponse> Handle(CustomerWithOrdersRequest request, CancellationToken cancellationToken)
        {
            var customer = await context.Customers
                .Where(x => x.Id == request.CustomerId).Include(c => c.Orders).FirstOrDefaultAsync();

            var result = mapper.Map<Customer, CustomerWithOrdersResponse>(customer);

            return result;
        }
    }
}
