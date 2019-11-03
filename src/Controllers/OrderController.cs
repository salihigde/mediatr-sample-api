using System.Threading.Tasks;
using MediatrSampleApi.Handlers.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MediatrSampleApi.Handlers.Contracts;

namespace MediatrSampleApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Creates order by given new order information
        /// </summary>
        /// <param name="order"></param>
        /// <response code="200">Retrieves order id to indicate succesful creation</response>
        /// <response code="400">Something went wrong while creating order</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<OrderCreateResponse>), 201)]
        public async Task<IActionResult> CreateOrderAsync([FromBody]OrderRequest order)
        {
            var result = await mediator.Send(order);

            return CreatedAtAction(nameof(CreateOrderAsync), null, result);
        }
    }
}
