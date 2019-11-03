using MediatrSampleApi.Handlers.Contracts;
using MediatrSampleApi.Models;
using AutoMapper;

namespace MediatrSampleApi.Handlers.Mappers
{
    /// <summary>
    /// </summary>
    public class OrderToOrderContract : Profile
    {
        /// <summary>
        /// </summary>
        public OrderToOrderContract()
        {
            CreateMap<Order, OrderResponse>(MemberList.Source);
        }
    }
}
