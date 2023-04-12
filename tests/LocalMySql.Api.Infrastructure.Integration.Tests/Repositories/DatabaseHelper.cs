using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using LocalMySql.Api.Infrastructure.Configuration;
using MySql.Data.MySqlClient;

namespace LocalMySql.Api.Infrastructure.Integration.Tests.Repositories
{
    public class DatabaseHelper<TId, TRecord>
    {
        private readonly string _tableName;
        private readonly string _idColumnName;
        private readonly string _connectionString;

        public DatabaseHelper(
            string tableName,
            string idColumnName,
            DatabaseOptions options)
        {
            _tableName = tableName;
            _idColumnName = idColumnName;
            _connectionString = $"Server={options.Server};Port={options.Port ?? 3306};Database={options.Schema};Uid={options.User};Pwd={options.Password};Allow User Variables=True";
        }

        public async Task<T> GetRecordAsync<T>(TId id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                return await connection.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {_tableName} WHERE {_idColumnName} = @Id LIMIT 1", new { id });
            }
        }

        public async Task AddRecordAsync(TRecord record)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                var properties = record.GetType().GetProperties().Select(x => x.Name).ToList();

                var sql = $@"INSERT INTO Customers 
                         ({string.Join(",", properties)})
                         VALUES ({string.Join(",", properties.Select(x => $"@{x}"))})";

                await connection.ExecuteAsync(sql, ToDynamicParameters(record), commandType: CommandType.Text);
            }
        }

        private static DynamicParameters ToDynamicParameters<T>(T record)
        {
            if (record == null)
            {
                return null;
            }
            
            var recordType = record.GetType();
            var properties = recordType.GetProperties();

            var nonEnumPropertiesByName = properties.Where(pi => !pi.PropertyType.IsEnum).Select(x => new KeyValuePair<string, object>(x.Name, x.GetValue(record)));
            var enumProperties = properties.Where(pi => pi.PropertyType.IsEnum);
            var valuesByPropertyName = new List<KeyValuePair<string, object>>(nonEnumPropertiesByName);
            valuesByPropertyName.AddRange(enumProperties.Select(ep => new KeyValuePair<string, object>(ep.Name, ep.GetValue(record)?.ToString())));

            return new DynamicParameters(valuesByPropertyName);
        }
    }
}
