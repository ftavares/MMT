using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMT.Application.Orders.Queries.GetOrders
{


    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidator()
        {
            RuleFor(e => e.CustomerId).NotEmpty().NotNull();
            RuleFor(e => e.User).EmailAddress();
        }
    }
}
