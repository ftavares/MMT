using AutoMapper;
using MMT.Application.Common.Mappings;
using MMT.Domain.Models.CustomerDetailAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Models
{
    public class CustomerDto : IMapFrom<CustomerDetails>
    {
        [JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CustomerDetails, CustomerDto>();
        }
    }
}
