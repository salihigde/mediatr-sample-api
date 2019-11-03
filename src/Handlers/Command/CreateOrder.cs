using System;
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
    public class OrderRequest : IRequest<OrderCreateResponse>
    {
        /// <summary>
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// </summary>
        public decimal Price { get; set; }
    }

    /// <summary>
    /// </summary>
    public class OrderRequestValidator : AbstractValidator<OrderRequest>
    {
        /// <summary>
        /// </summary>
        public OrderRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotNull().NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
        }
    }

    /// <summary>
    /// Create order handler
    /// </summary>
    public class CreateOrder : IRequestHandler<OrderRequest, OrderCreateResponse>
    {
        private readonly ApiDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public CreateOrder(ApiDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates order
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>returns customer id of the new customer</returns>
        public async Task<OrderCreateResponse> Handle(OrderRequest request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<OrderRequest, Order>(request);

            context.Orders.Add(order);

            await context.SaveChangesAsync(cancellationToken);

            return new OrderCreateResponse { Id = order.Id };
        }
    }
}
