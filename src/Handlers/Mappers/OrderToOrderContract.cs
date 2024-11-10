using AutoMapper;
using MediatrSample.Api.Models;
using MediatrSample.Api.ViewModels;

namespace MediatrSample.Api.Handlers.Mappers
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
