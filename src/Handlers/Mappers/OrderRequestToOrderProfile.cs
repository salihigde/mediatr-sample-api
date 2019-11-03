using System;
using MediatrSampleApi.Models;
using MediatrSampleApi.Handlers.Command;
using AutoMapper;

namespace MediatrSampleApi.Handlers.Mappers
{
    /// <summary>
    /// </summary>
    public class OrderRequestToOrderProfile : Profile
    {
        /// <summary>
        /// </summary>
        public OrderRequestToOrderProfile()
        {
            CreateMap<OrderRequest, Order>(MemberList.Source)
                .AfterMap((_, dest) => dest.CreatedDate = DateTime.Now);
        }
    }
}
