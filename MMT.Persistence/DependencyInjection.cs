using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MMT.Persistence.Entities;

namespace MMT.Persistence
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<SSE_TestContext>(options => options.UseSqlServer(Configuration.GetConnectionString("connectionString")));
            return services;
        }

    }
}
