using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClassLibrary1MTKDotNetCore.Shared
{
    public class ADOService
    {
        private readonly string _connectionString;

        public ADOService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<G> Query<G>(string query)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            connection.Close();

            string json = JsonConvert.SerializeObject(dt);
            List<G> list = JsonConvert.DeserializeObject<List<G>>(json);
            return list;
        }
    }
}
