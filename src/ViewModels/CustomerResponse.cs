using System;

namespace MediatrSample.Api.ViewModels
{
    /// <summary>
    /// customer contract which is exposed to client
    /// </summary>
    public class CustomerResponse
    {
        /// <summary>
        /// Customer id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        public string Email { get; set; }
    }
}
