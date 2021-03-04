using MMT.Domain.Models.CustomerDetailAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Services
{
    public class CustomerDetailsService
    {

        public HttpClient _client { get; }

        public CustomerDetailsService(HttpClient client)
        {
            _client = client;
        }

        public virtual async Task<CustomerDetails> GetCustomerDetails(GetCustomerDetailsRequest request)
        {
            var jsonObj = JsonConvert.SerializeObject(request, Formatting.Indented);
            var requestContent = new StringContent(jsonObj, Encoding.UTF8, "application/json");

            var codes = _client.DefaultRequestHeaders.GetValues("code");
            var query = "";

            if (codes.Any())
            {
                query = $"code={codes.First()}";

                _client.DefaultRequestHeaders.Remove("code");
            }

            var response = await _client.PostAsync($"GetUserDetails?{query}", requestContent);

            var content = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CustomerDetails>(content);

        }


    }


}
