namespace MTKDotNetCore.PizzaAPI.Queries
{
    public class PizzaQuery
    {
        public static string PizzaOrderQuery { get; } =
            @"select po.* , p.Pizza, p.Price from [dbo] . [Tbl_PizzaOrder] po
            inner join Tbl_Pizza  p on p.PizzaId = po.PizzaId
            where PizzaOrderInoviceNo = @PizzaOrderInoviceNo";

        public static string PizzaOrderDetailQuery { get; } =
            @"select pod.*,px.PizzaExtraName,px.Price from [dbo] . [Tbl_PizzaOrderDetailModel] pod
             inner join Tbl_PizzaExtra px on px.PizzaExtraId = pod.PizzaExtraId
             where PizzaOrderInoviceNo = @PizzaOrderInoviceNo";
    }
}