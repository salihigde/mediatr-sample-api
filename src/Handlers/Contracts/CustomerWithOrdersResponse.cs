using System.Collections.Generic;

namespace MediatrSampleApi.Handlers.Contracts
{
    /// <summary>
    /// customer with details
    /// </summary>
    public class CustomerWithOrdersResponse : CustomerResponse
    {
        /// <summary>
        /// </summary>
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}
