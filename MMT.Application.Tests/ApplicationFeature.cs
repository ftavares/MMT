using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MMT.Application.Orders.Queries.GetOrders;
using MMT.Application.Services;
using MMT.Domain.Models.CustomerDetailAPI;
using MMT.Persistence.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Tests
{
    public class ApplicationFeature : IDisposable
    {
        IMapper _mapper;
        SSE_TestContext _context;
        ILogger<GetOrderQueryHandler> _logger;
        CustomerDetailsService _customerDetailsService;
        public GetOrderQueryHandler _handler;

        public ApplicationFeature()
        {
            _mapper = GetMapper();
            _context = GetContext();
            _logger = new Mock<ILogger<GetOrderQueryHandler>>().Object;
            _customerDetailsService = GetCustomerDetailsService();
            _handler = new GetOrderQueryHandler(_logger, _customerDetailsService, _context, _mapper);
        }

        public void Dispose()
        {
            _mapper = null;
            _context.Dispose();
            _logger = null;
            _customerDetailsService = null;
        }

        private IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(Assembly.Load("MMT.Application"))
            );

            return new Mapper(configuration);
        }

        private CustomerDetailsService GetCustomerDetailsService()
        {
            var data = new List<CustomerDetails> { new CustomerDetails()
            {
                Email = "bob@mmtdigital.co.uk",
                CustomerId = "R223232",
                Website = true,
                FirstName = "Bob",
                LastName = "Testperson",
                LastLoggedIn = new DateTime(2020, 01, 01),
                HouseNumber = "1a",
                Street = "Uppingham Gate",
                Town = "Uppingham",
                Postcode = "LE15 9NY",
                PreferredLanguage = "en-gb"

            }};

            var customerDetailsService = new Mock<CustomerDetailsService>(new HttpClient());

            customerDetailsService.Setup(s =>
            s.GetCustomerDetails(It.IsAny<GetCustomerDetailsRequest>())).Returns(
                (GetCustomerDetailsRequest request) =>
                {
                    return Task.FromResult(data.FirstOrDefault(e => e.Email == request.Email));
                });

            return customerDetailsService.Object;
        }

        private SSE_TestContext GetContext()
        {
            var options = new DbContextOptionsBuilder<SSE_TestContext>()
                .UseInMemoryDatabase(databaseName: "SSE_TestContext")
                .Options;

            var ordersData = new List<Order>
            {
                new Order
                {
                    Orderid =1,
                    Customerid = "R223232",
                    Orderdate = new DateTime(2020,1,1),
                    Deliveryexpected = new DateTime(2020,1,1),
                    Containsgift=false,
                    Shippingmode = "Post",
                    Ordersource ="Web",
                },
                new Order
                {
                    Orderid =2,
                    Customerid = "R223232",
                    Orderdate = new DateTime(2020,1,2),
                    Deliveryexpected = new DateTime(2020,1,2),
                    Containsgift=false,
                    Shippingmode = "Post",
                    Ordersource ="Web",
                }
            };

            var productData = new List<Product>()
            {
                new Product() { Productid=1, Colour="red", Packheight=1, Packweight=1, Productname="Product1", Packwidth=1, Size="xl"},
                new Product() { Productid=2, Colour="blue", Packheight=1, Packweight=1, Productname="Product2", Packwidth=1, Size="l"},
                new Product() { Productid=3, Colour="green", Packheight=1, Packweight=1, Productname="Product3", Packwidth=1, Size="s"}
            };

            var orderItemsData = new List<Orderitem>()
            {
                new Orderitem{
                    Orderitemid=1,
                    Orderid = 1,
                    Quantity =1,
                    Price =1,
                    Returnable =false,
                    Productid=1,
                },
                new Orderitem
                {
                    Orderitemid=2,
                    Orderid = 1,
                    Quantity =1,
                    Price =1,
                    Returnable =false,
                    Productid=2,
                },
                new Orderitem
                {
                    Orderitemid=3,
                    Orderid = 2,
                    Quantity =1,
                    Price =1,
                    Returnable =false,
                    Productid=1,
                },
                new Orderitem
                {
                    Orderitemid=4,
                    Orderid = 2,
                    Quantity =1,
                    Price =1,
                    Returnable =false,
                    Productid=3,
                }

            };


            var context = new SSE_TestContext(options);

            context.Products.AddRange(productData);
            context.Orders.AddRange(ordersData);
            context.Orderitems.AddRange(orderItemsData);

            context.SaveChanges();


            return context;

        }

    }
}
