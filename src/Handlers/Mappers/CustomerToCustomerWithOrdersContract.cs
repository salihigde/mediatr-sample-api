using MediatrSampleApi.Handlers.Contracts;
using MediatrSampleApi.Models;
using AutoMapper;

namespace MediatrSampleApi.Handlers.Mappers
{
    /// <summary>
    /// </summary>
    public class CustomerToCustomerWithOrdersContract : Profile
    {
        /// <summary>
        /// </summary>
        public CustomerToCustomerWithOrdersContract()
        {
            CreateMap<Customer, CustomerWithOrdersResponse>(MemberList.Source);
        }
    }
}
