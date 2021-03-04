using AutoMapper;
using AutoMapper.Configuration.Conventions;
using MMT.Application.Common.Mappings;
using MMT.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Models
{

    public class ProductDto : IMapFrom<Product>
    {
        [MapTo("Productname")]
        public string Productname { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Product, ProductDto>();
        }
    }
}
