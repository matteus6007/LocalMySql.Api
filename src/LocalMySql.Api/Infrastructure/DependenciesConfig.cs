using LocalMySql.Api.Contracts.Models;
using LocalMySql.Api.Domain.Services;
using LocalMySql.Api.Infrastructure.Mappers;
using LocalMySql.Api.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocalMySql.Api.Infrastructure
{
    public static class DependenciesConfig
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICustomerService, CustomerService>();
            services.AddSingleton<IMapper<Customer, CustomerV1>, CustomerMapper>();
        }
    }
}
