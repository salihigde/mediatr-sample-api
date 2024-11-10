using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using MediatrSample.Api.Models;
using MediatrSample.Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace MediatrSample.Api.Handlers.Query
{
    /// <summary>
    /// </summary>
    public class CustomerListRequest : IRequest<IEnumerable<CustomerResponse>>
    {

    }

    /// <summary>
    /// get all customers handler
    /// </summary>
    public class GetCustomers : IRequestHandler<CustomerListRequest, IEnumerable<CustomerResponse>>
    {
        private readonly ApiDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public GetCustomers(ApiDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// gets all the customers
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>returns list of customers</returns>
        public async Task<IEnumerable<CustomerResponse>> Handle(CustomerListRequest request, CancellationToken cancellationToken)
        {
            var customers = await context.Customers.ToListAsync();

            var result = mapper.Map<List<Customer>, IEnumerable<CustomerResponse>>(customers);

            return result;
        }
    }
}
