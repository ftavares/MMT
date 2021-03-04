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
    public class OrderDto : IMapFrom<Order>
    {
        [JsonProperty(PropertyName = "orderNumber")]
        public int OrderNumber { get; set; }


        [MapTo("OrderDate")]
        [JsonIgnore]
        public DateTime _orderDate { get; set; }

        [JsonProperty(PropertyName = "orderDate")]
        public string OrderDate => _orderDate.ToString("dd-MMM-yyyy");


        [JsonProperty(PropertyName = "deliveryAddress")]
        public string DeliveryAddress { get; set; }


        [JsonIgnore]
        [MapTo("Containsgift")]
        public bool _containsgift { get; set; }


        [MapTo("OrderItems")]
        [JsonIgnore]
        public IEnumerable<OrderItemDto> _orderItems { get; set; }

        [JsonProperty(PropertyName = "orderItems")]
        public IEnumerable<OrderItemDto> OrderItems => CheckGifts(_orderItems);



        [MapTo("DeliveryExpected")]
        [JsonIgnore]
        public DateTime _deliveryExpected { get; set; }

        [JsonProperty(PropertyName = "deliveryExpected")]
        public string DeliveryExpecte => _deliveryExpected.ToString("dd-MMM-yyyy");


        private IEnumerable<OrderItemDto> CheckGifts(IEnumerable<OrderItemDto> items)
        {

            foreach (var item in items)
            {
                item.ProductName = (_containsgift) ? "Gift" : item._product.Productname;
            }

            return items;
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, OrderDto>();
        }
    }
}



