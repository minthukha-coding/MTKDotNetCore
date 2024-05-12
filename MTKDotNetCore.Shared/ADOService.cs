using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1MTKDotNetCore.Shared
{
    public class ADOService
    {
        private readonly string _connectionString;

        public ADOService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<G> Query<T>(string query)
        {

        }
    }
}
