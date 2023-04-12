using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LocalMySql.Api.Contracts.Models;

namespace LocalMySql.Api.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<CustomerV1> _repository;

        public CustomerService(IRepository<CustomerV1> repository)
        {
            _repository = repository;
        }

        public async Task SaveAsync(CustomerV1 customer)
        {
            await _repository.SaveAsync<CustomerV1>(customer);
        }

        public async Task<CustomerV1> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<CustomerV1>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
