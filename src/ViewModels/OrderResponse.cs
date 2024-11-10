using System;

namespace MediatrSample.Api.ViewModels
{
    /// <summary>
    /// order object which is exposed to client
    /// </summary>
    public class OrderResponse
    {
        /// <summary>
        /// order id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// </summary>
        public DateTime CreatedDate { get; set; }
    }
}
