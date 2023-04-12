using LocalMySql.Api.Contracts.Models;
using LocalMySql.Api.Domain.Services;
using LocalMySql.Api.Infrastructure.Models;

namespace LocalMySql.Api.Infrastructure.Mappers
{
    public class CustomerMapper : IMapper<Customer, CustomerV1>
    {
        public CustomerV1 Map(Customer data)
        {
            if (data == null)
            {
                return null;
            }

            return new CustomerV1
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                CreatedOn = data.CreatedOn
            };
        }
    }
}
