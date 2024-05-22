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
    public DbSet<PizzaExtraModel> PizzaExtrass { get; set; }
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

[Table("Tbl_PizzaExtra")]
public class PizzaExtraModel()
{
    [Key]
    [Column("PizzaExtraId")]
    public int ID { get; set; }
    [Column("PizzaExtraName")]
    public string Name { get; set; }
    [Column("Price")]
    public decimal Price { get; set; }
    [NotMapped]
    public string PriceString { get { return "$ " + Price; } }
}

public class OrderRequest()
{
    public int PizzaId { get; set; }
    public int[] Extras { get; set; }
}

[Table("Tbl_PizzaOrder")]
public class PizzaOrderModel()
{
    [Key]
    public int PizzaOrderId { get; set; }
    public string PizzaOrderInoviceNo { get; set; }
    public int PizzaId { get; set; }
    public decimal TotalAmount { get; set; }
}

[Table("Tbl_PizzaOrderDetailModel")]
public class PizzaOrderDetailModel()
{
    [Key]
    public int PizzaOrderDetailId { get; set; }
    public string PizzaOrderInoviceNo { get; set; }
    public int PizzaExtraId { get; set; }
}