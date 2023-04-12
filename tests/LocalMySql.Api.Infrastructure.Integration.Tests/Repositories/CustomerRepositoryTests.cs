using System;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using LocalMySql.Api.Contracts.Models;
using LocalMySql.Api.Domain.Services;
using LocalMySql.Api.Infrastructure.Configuration;
using LocalMySql.Api.Infrastructure.Mappers;
using LocalMySql.Api.Infrastructure.Models;
using LocalMySql.Api.Infrastructure.Repositories.MySql;
using Shouldly;
using Xunit;

namespace LocalMySql.Api.Infrastructure.Integration.Tests.Repositories
{
    public class CustomerRepositoryTests
    {
        private readonly DatabaseHelper<Guid, Customer> _databaseHelper;
        private readonly IMapper<Customer, CustomerV1> _mapper;
        private readonly IRepository<CustomerV1> _sut;

        public CustomerRepositoryTests()
        {
            var options = new DatabaseOptions
            {
                Server = "localhost",
                Port = 30001,
                Schema = "CloudcallContacts",
                User = "root",
                Password = "cloudcall123"
            };

            _databaseHelper = new DatabaseHelper<Guid, Customer>("Customers", "Id", options);

            _mapper = new CustomerMapper();

            _sut = new CustomerRepository(_mapper, options);
        }

        [Theory]
        [AutoData]
        public async Task SaveAsync_WhenCustomerDoesNotExist_ThenSaveIsSuccessful(CustomerV1 customer)
        {
            // Act
            await _sut.SaveAsync<CustomerV1>(customer);

            // Assert
            var result = await ThenCustomerExists(customer.Id);

            result.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public async Task GetByIdAsync_WhenCustomerExists_ThenShouldReturnCustomer(Customer customer)
        {
            // Arrange
            await GivenCustomerExists(customer);

            // Act
            var result = await _sut.GetByIdAsync(customer.Id);

            // Assert
            result.ShouldNotBeNull();
            result.Id.ShouldBe(customer.Id);
            result.FirstName.ShouldBe(customer.FirstName);
            result.LastName.ShouldBe(customer.LastName);
            result.CreatedOn.ShouldBe(customer.CreatedOn, TimeSpan.FromSeconds(1));
        }

        [Theory]
        [AutoData]
        public async Task GetByIdAsync_WhenCustomerDoesNotExist_ThenShouldReturnNull(Guid id)
        {
            // Act
            var result = await _sut.GetByIdAsync(id);

            // Assert
            result.ShouldBeNull();
        }

        private async Task<CustomerV1> ThenCustomerExists(Guid id)
        {
            var customer = await _databaseHelper.GetRecordAsync<Customer>(id);

            return _mapper.Map(customer);
        }

        private async Task GivenCustomerExists(Customer customer)
        {
            await _databaseHelper.AddRecordAsync(customer);
        }
    }
}
