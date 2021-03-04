﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Domain.Models.CustomerDetailAPI
{
    public class CustomerDetails
    {
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public bool Website { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string PreferredLanguage { get; set; }

        public string Address => $"{Street}, {Town}, {Postcode}";
    }


}
