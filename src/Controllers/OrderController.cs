using System.Threading.Tasks;
using MediatR;
using MediatrSample.Api.Handlers.Command;
using MediatrSample.Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MediatrSampleApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
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
            var result = await _mediator.Send(order);

            return Created(string.Empty, result);
        }
    }
}
