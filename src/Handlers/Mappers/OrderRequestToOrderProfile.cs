using System;
using AutoMapper;
using MediatrSample.Api.Handlers.Command;
using MediatrSample.Api.Models;

namespace MediatrSample.Api.Handlers.Mappers
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
