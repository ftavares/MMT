using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Queries.GetOrders
{
    public class GetOrderQuery : IRequest<GetOrderResult>
    {
        public string User { get; set; }
        public string CustomerId { get; set; }
    }
}
