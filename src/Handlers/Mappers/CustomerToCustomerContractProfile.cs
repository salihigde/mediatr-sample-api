using MediatrSampleApi.Handlers.Contracts;
using MediatrSampleApi.Models;
using AutoMapper;

namespace MediatrSampleApi.Handlers.Mappers
{
    /// <summary>
    /// </summary>
    public class CustomerToCustomerContractProfile : Profile
    {
        /// <summary>
        /// </summary>
        public CustomerToCustomerContractProfile()
        {
            CreateMap<Customer, CustomerResponse>(MemberList.Source);
        }
    }
}
