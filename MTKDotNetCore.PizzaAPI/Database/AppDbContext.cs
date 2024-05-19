using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTKDotNetCore.PizzaAPI.Database;

internal class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
    }
    public DbSet<PizzaModel> Pizzas { get; set; }
}

[Table("Tbl_Pizza")]
public class PizzaModel()
{
    [Key]
    [Column("PizzaId")]
    public int ID { get; set; }
    [Column("Pizza")]
    public string Name { get; set; } 
    [Column("Price")]
    public decimal PizzaPrice { get; set; }
}

