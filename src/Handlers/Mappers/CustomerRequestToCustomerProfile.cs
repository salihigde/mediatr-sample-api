using System;
using AutoMapper;
using MediatrSample.Api.Handlers.Command;
using MediatrSample.Api.Models;

namespace MediatrSample.Api.Handlers.Mappers
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
