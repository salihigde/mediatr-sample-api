using System;

namespace MediatrSample.Api.ViewModels
{
    /// <summary>
    /// returned in successful customer creation
    /// </summary>
    public class CustomerCreateResponse
    {
        /// <summary>
        /// customer id
        /// </summary>
        public Guid Id { get; set; }
    }
}
