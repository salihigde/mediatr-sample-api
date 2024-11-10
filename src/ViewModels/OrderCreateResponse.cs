using System;

namespace MediatrSample.Api.ViewModels
{
    /// <summary>
    /// returned in successful order creation
    /// </summary>
    public class OrderCreateResponse
    {
        /// <summary>
        /// order id
        /// </summary>
        public Guid Id { get; set; }
    }
}
