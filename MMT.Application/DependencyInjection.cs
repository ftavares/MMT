using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MMT.Application.Services;
using MMT.Domain.Common.Behaviours;
using System;
using System.Reflection;


namespace MMT.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            services.AddHttpClient<CustomerDetailsService>(client =>
            {
                client.BaseAddress = new Uri(Configuration.GetValue<string>("CustomerDetailsApi:BaseUrl"));
                client.DefaultRequestHeaders.Add("code", Configuration.GetValue<string>("CustomerDetailsApi:Key"));
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            return services;
        }
    }
}
