using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.Extensions.Logging;
using MMT.Application.Orders.Models;
using MMT.Application.Services;
using MMT.Domain.Common.Exceptions;
using MMT.Domain.Models.CustomerDetailAPI;
using MMT.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Queries.GetOrders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, GetOrderResult>
    {
        private readonly ILogger<GetOrderQueryHandler> _logger;
        private readonly SSE_TestContext _context;
        private readonly IMapper _mapper;
        private readonly CustomerDetailsService _customerDetailsService;

        public GetOrderQueryHandler(ILogger<GetOrderQueryHandler> logger, CustomerDetailsService customerDetailsService, SSE_TestContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _customerDetailsService = customerDetailsService;
        }

        public async Task<GetOrderResult> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {

            var customerDetails = await _customerDetailsService.GetCustomerDetails(new GetCustomerDetailsRequest() { Email = request.User });

            if (customerDetails == null)
            {
                throw new EntityNotFoundException(nameof(CustomerDetails), request.User);
            }

            var order = _context.Orders.Where(e => e.Customerid == request.CustomerId).OrderByDescending(e => e.Orderdate).Take(1).FirstOrDefault();

            if (order == null)
            {
                var c = _mapper.Map<CustomerDto>(customerDetails);
                return new GetOrderResult()
                {
                    Customer =c
                };
            }

            _context.Entry(order).Collection(e => e.Orderitems).Load();

            foreach (var orderItem in order.Orderitems)
            {
                _context.Entry(orderItem).Reference(e => e.Product).Load();
            }

            var result = new GetOrderResult()
            {
                Customer = _mapper.Map<CustomerDto>(customerDetails),
                Order = _mapper.Map<OrderDto>(order),
            };

            result.Order.DeliveryAddress = customerDetails.Address;

            return result;
        }

    }

}
