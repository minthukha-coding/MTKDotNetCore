using System.Data.SqlClient;

namespace MTKDotNetCore.PizzaAPI
{
    public static class ConnectionString
    {
        public static SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "MTKDotNetCore",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };
    }
}
