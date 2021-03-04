using MMT.Application.Orders.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Queries.GetOrders
{
    public class GetOrderResult
    {
        [JsonProperty(PropertyName = "customer")]
        public CustomerDto Customer {get;set;}

        [JsonProperty(PropertyName = "order")]
        public OrderDto Order { get; set; }
	}

}
