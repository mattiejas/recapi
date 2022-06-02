using AutoMapper;
using Recapi.Domain.Entities;
using Recapi.Application.Common.Mappings;

namespace Recapi.Application.Customers.Queries.GetCustomersList
{
    public class CustomerLookupDto : IMapFrom<Customer>
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Customer, CustomerLookupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CustomerId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
        }
    }
}
