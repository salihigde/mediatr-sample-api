using AutoMapper;
using MediatrSample.Api.Models;
using MediatrSample.Api.ViewModels;

namespace MediatrSample.Api.Handlers.Mappers
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
