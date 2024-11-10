using System.Collections.Generic;

namespace MediatrSample.Api.ViewModels
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
