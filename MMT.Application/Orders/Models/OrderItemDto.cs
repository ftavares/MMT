using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MMT.Application.Common.Mappings;
using MMT.Persistence.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Models
{
    public class OrderItemDto : IMapFrom<Orderitem>
    {

        [MapTo("Product")]
        [JsonIgnore]
        public ProductDto _product { get; set; }

        [JsonProperty(PropertyName = "product")]
        public string ProductName { get; set; }


        [JsonProperty(PropertyName = "quantity")]
        public int Quantity { get; set; }

        [JsonProperty(PropertyName = "priceEach")]
        public decimal Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Orderitem, OrderItemDto>();
        }
    }
}

