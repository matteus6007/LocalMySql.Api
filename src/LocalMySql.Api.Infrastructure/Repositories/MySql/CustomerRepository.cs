using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LocalMySql.Api.Contracts.Models;
using LocalMySql.Api.Domain.Services;
using LocalMySql.Api.Infrastructure.Configuration;
using LocalMySql.Api.Infrastructure.Models;
using MySql.Data.MySqlClient;

namespace LocalMySql.Api.Infrastructure.Repositories.MySql
{
    public class CustomerRepository : IRepository<CustomerV1>
    {
        private readonly IMapper<Customer, CustomerV1> _mapper;
        private readonly string _connectionString;

        public CustomerRepository(IMapper<Customer, CustomerV1> mapper, DatabaseOptions options)
        {
            _mapper = mapper;
            _connectionString = $"Server={options.Server};Port={options.Port ?? 3306};Database={options.Schema};Uid={options.User};Pwd={options.Password};Allow User Variables=True";
        }

        public async Task SaveAsync<T>(CustomerV1 item)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "INSERT INTO Customers (Id,FirstName,LastName,CreatedOn) VALUES (@Id,@FirstName,@LastName,@CreatedOn);";

                var parameters = new
                {
                    item.Id,
                    item.FirstName,
                    item.LastName,
                    item.CreatedOn
                };

                await connection.ExecuteAsync(sql, parameters, commandType: CommandType.Text);
            }
        }

        public async Task<CustomerV1> GetByIdAsync(Guid id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "SELECT Id,FirstName,LastName,CreatedOn FROM Customers WHERE Id = @Id;";

                var parameters = new
                {
                    Id = id
                };

                var customer = await connection.QueryFirstOrDefaultAsync<Customer>(sql, parameters, commandType: CommandType.Text);

                return _mapper.Map(customer);
            }
        }

        public async Task<List<CustomerV1>> GetAll()
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                const string sql = "SELECT Id,FirstName,LastName,CreatedOn FROM Customers;";

                var customers = await connection.QueryAsync<Customer>(sql, commandType: CommandType.Text);

                return customers.Select(_mapper.Map).ToList();
            }
        }
    }
}
