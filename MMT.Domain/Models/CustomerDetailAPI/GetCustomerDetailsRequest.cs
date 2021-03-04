using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Domain.Models.CustomerDetailAPI
{
    public class GetCustomerDetailsRequest
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
