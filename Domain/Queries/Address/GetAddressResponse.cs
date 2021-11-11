using Domain.Models;
using FeaturesAPI.Domain.Models;

namespace Domain.Queries.Address
{
    public class GetAddressResponse : CommandResponse
    {
        public AddressData Address { get; set; }
    }
}
