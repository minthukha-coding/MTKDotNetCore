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
        public class ADOParameter
        {
            public ADOParameter()
            {
            }
            public ADOParameter(string name, object value)
            {
                Name = name;
                Value = value;
            }
            public string Name { get; set; }
            public object Value { get; set; }
        }
        public List<G> Query<G>(string query, ADOParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            if (parameters is not null && parameters.Length > 0)
            {
                //foreach (var item in parameters)
                //{
                //    command.Parameters.AddWithValue(item.Name, item.Value);
                //}
                command.Parameters.AddRange(parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray());
            }
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
