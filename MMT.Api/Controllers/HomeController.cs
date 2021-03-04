using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MMT.Application.Orders.Queries.GetOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MMT.Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMediator mediator, ILogger<HomeController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [Route("/")]
        public async Task<GetOrderResult> GetOrder([FromBody] GetOrderQuery query)
        {
            _logger.LogInformation("GetOrder Endpoint of HomeController Called");
            var result = await _mediator.Send(query);
            return result;
        }

    }
}
