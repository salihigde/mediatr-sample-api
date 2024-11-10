using AutoMapper;
using MediatrSample.Api.Models;
using MediatrSample.Api.ViewModels;

namespace MediatrSample.Api.Handlers.Mappers
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
