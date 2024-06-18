using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace ClassLibrary1MTKDotNetCore.Shared;

public class DapperService
{
    private readonly string _connectionString;

    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public List<G> Query<G>(string query,object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var lst = db.Query<G>(query,param).ToList();
        return lst;
    }  

    public G QueryFirstOrDefault<G>(string query,object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var item = db.Query<G>(query,param).FirstOrDefault();
        return item!;
    }

    public int Excute(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var result =  db.Execute(query, param);
        return result;
    }
}