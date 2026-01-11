using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace TokoApi.Helpers
{
    public class koneksi
    {
        private readonly IConfiguration _configuration;
        public koneksi(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public SqlConnection GetConnection()
        {
            var connectionString =
            _configuration.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }
    }
}