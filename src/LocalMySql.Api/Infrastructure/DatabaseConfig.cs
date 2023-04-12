using LocalMySql.Api.Contracts.Models;
using LocalMySql.Api.Domain.Services;
using LocalMySql.Api.Infrastructure.Configuration;
using LocalMySql.Api.Infrastructure.Repositories.MySql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalMySql.Api.Infrastructure
{
    public static class DatabaseConfig
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(x =>
                configuration.GetSection("Cloudcall:Database").Get<DatabaseOptions>());

            services.AddSingleton<IRepository<CustomerV1>, CustomerRepository>();
        }
    }
}
