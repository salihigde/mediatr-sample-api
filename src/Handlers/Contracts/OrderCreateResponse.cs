using System;
namespace MediatrSampleApi.Handlers.Contracts
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
