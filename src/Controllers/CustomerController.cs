using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MediatrSample.Api.Exceptions;
using MediatrSample.Api.Handlers.Command;
using MediatrSample.Api.Handlers.Query;
using MediatrSample.Api.ViewModels;

namespace MediatrSample.Api.Controllers
{
    /// <summary>
    /// </summary>
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Retrieves all the customers
        /// </summary>
        /// <response code="200">Retrieves all the customers</response>
        /// <response code="400">Something went wrong while retrieving customers</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CustomerResponse>>), 200)]
        [Route("list")]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await _mediator.Send(new CustomerListRequest());

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a customer with all of the orders by given customer id
        /// </summary>
        /// <response code="200">Retrieves a customer with all of the orders</response>
        /// <response code="400">Something went wrong while retrieving customer with orders</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<CustomerWithOrdersResponse>), 200)]
        [Route("{customerId}/details")]
        public async Task<IActionResult> GetCustomerWithOrdersAsync([FromRoute] Guid customerId)
        {
            var result = await _mediator.Send(new CustomerWithOrdersRequest { CustomerId = customerId });

            return Ok(result);
        }

        /// <summary>
        /// Creates customer by given new customer information
        /// </summary>
        /// <param name="customer"></param>
        /// <response code="200">Retrieves customer id to indicate succesful creation</response>
        /// <response code="400">Something went wrong while creating customer</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CustomerCreateResponse>), 201)]
        public async Task<IActionResult> CreateCustomerAsync([FromBody]CustomerRequest customer)
        {
            var customerExists = await _mediator.Send(new DoesCustomerExistsRequest { Email = customer.Email });
            if (customerExists)
            {
                throw new ValidationException("Customer already exists");
            }

            var result = await _mediator.Send(customer);
            return Created(string.Empty, result);
        }
    }
}
