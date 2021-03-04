using Moq;
using System;
using Xunit;
using MMT.Application.Services;
using MMT.Domain.Models.CustomerDetailAPI;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMT.Persistence.Entities;
using System.Collections.Generic;
using System.Linq;
using MMT.Application.Orders.Queries.GetOrders;
using Microsoft.Extensions.Logging;
using AutoMapper;
using System.Threading;
using FluentAssertions;
using System.Net.Http;
using MMT.Domain.Common.Exceptions;
using System.Reflection;

namespace MMT.Application.Tests
{
    public class GetOrderHandle : IClassFixture<ApplicationFeature>
    {

        public GetOrderQueryHandler _handler;

        public GetOrderHandle(ApplicationFeature fixture)
        {
            this._handler = fixture._handler;
        }

        [Fact]
        public async void CustomerDontExist()
        {
            Func<Task> act = () => _handler.Handle(new GetOrderQuery() { CustomerId = "", User = "" }, new CancellationToken());
            var ex = await Record.ExceptionAsync(act);

            ex.Should().NotBeNull();
            Assert.IsType<EntityNotFoundException>(ex);

        }

        [Fact]
        public async void CustomerExistsNoUser()
        {
            var result = await _handler.Handle(new GetOrderQuery() { CustomerId = "", User = "bob@mmtdigital.co.uk" }, new CancellationToken());

            result.Should().NotBeNull();
            result.Customer.Should().NotBeNull();
            result.Order.Should().BeNull();
        }

        [Fact]
        public async void CustomerExistsUserExists()
        {
            var result = await _handler.Handle(new GetOrderQuery() { CustomerId = "R223232", User = "bob@mmtdigital.co.uk" }, new CancellationToken());

            result.Should().NotBeNull();
            result.Customer.Should().NotBeNull();
            result.Order.Should().NotBeNull();
        }


  
    }

}
