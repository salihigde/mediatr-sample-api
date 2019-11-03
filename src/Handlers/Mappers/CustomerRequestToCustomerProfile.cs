using System;
using MediatrSampleApi.Models;
using MediatrSampleApi.Handlers.Command;
using AutoMapper;

namespace MediatrSampleApi.Handlers.Mappers
{
    /// <summary>
    /// </summary>
    public class CustomerRequestToCustomer : Profile
    {
        /// <summary>
        /// </summary>
        public CustomerRequestToCustomer()
        {
            CreateMap<CustomerRequest, Customer>(MemberList.Source)
                .AfterMap((_, dest) => dest.CreatedDate = DateTime.Now);
        }
    }
}
