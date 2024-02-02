#nullable disable

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.DataAccess
{
    public class DapperDbContext
    {
        private readonly string _connectionString;

        public DapperDbContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("InsuranceDb");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
