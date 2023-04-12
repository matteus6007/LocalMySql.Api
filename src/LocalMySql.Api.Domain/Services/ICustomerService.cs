using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalMySql.Api.Contracts.Models;

namespace LocalMySql.Api.Domain.Services
{
    public interface ICustomerService
    {
        Task SaveAsync(CustomerV1 customer);

        Task<CustomerV1> GetById(Guid id);

        Task<List<CustomerV1>> GetAll();
    }
}
