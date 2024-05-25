using ClassLibrary1MTKDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTKDotNetCore.PizzaAPI.Database;
using MTKDotNetCore.PizzaAPI.Queries;

namespace MTKDotNetCore.PizzaAPI.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly DapperService _dapperService;
        public PizzaController()
        {
            _appDbContext = new AppDbContext();
            _dapperService = new DapperService(ConnectionString.SqlConnectionStringBuilder.ConnectionString);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lst = await _appDbContext.Pizzas.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Extras")]
        public async Task<IActionResult> GetExtras()
        {
            var lst = await _appDbContext.PizzaExtrass.ToListAsync();
            return Ok(lst);
        }

        [HttpPost("Order")]
        public async Task<IActionResult> Order(OrderRequest reqModel)
        {
            var PizzaOrderInoviceNo = DateTime.Now.ToString("yyyyMMddHHmmss");
            var itemPizza = await _appDbContext.Pizzas.FirstOrDefaultAsync(x => x.ID == reqModel.PizzaId);
            var total = itemPizza.PizzaPrice;

            if (reqModel.Extras.Length > 0)
            {
                var lstExtra = await _appDbContext.PizzaExtrass.Where(x => reqModel.Extras.Contains(x.ID)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }

            PizzaOrderModel pizzaOrderModel = new PizzaOrderModel
            {
                PizzaId = reqModel.PizzaId,
                PizzaOrderInoviceNo = PizzaOrderInoviceNo,
                TotalAmount = total
            };

            List<PizzaOrderDetailModel> pizzaOrderDetailsModel = reqModel.Extras.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInoviceNo = PizzaOrderInoviceNo
            }).ToList();

            await _appDbContext.PizzaOrder.AddAsync(pizzaOrderModel);
            await _appDbContext.PizzaOrderDetail.AddRangeAsync(pizzaOrderDetailsModel);
            await _appDbContext.SaveChangesAsync();

            OrderRespnse respnse = new OrderRespnse()
            {
                InvoiceNo = PizzaOrderInoviceNo,
                Message = "Thank you for your order! Enjoy your pizza!",
                TotalAmount = total,
            };

            return (Ok(respnse));
        }

        #region Get PizaOreder With EF

        //[HttpGet("Order/{invoiceNo}")]
        //public async Task<IActionResult> GetOrder(string invoiceNo)
        //{
        //    var item = await _appDbContext.PizzaOrder.FirstOrDefaultAsync(x => x.PizzaOrderInoviceNo == invoiceNo);
        //    var lst = await _appDbContext.PizzaOrderDetail.Where(x => x.PizzaOrderInoviceNo == invoiceNo).ToListAsync();
        //    return Ok(
        //        new
        //        {
        //            Order = item,
        //            OrderDetail = lst
        //        });
        //}

        #endregion

        [HttpGet("Order/{invoiceNo}")]
        public IActionResult GetOrder(string invoiceNo)
        {
            var item = _dapperService.QueryFirstOrDefault<PizzaOrderInvoiceHeadModel>
                (
                    PizzaQuery.PizzaOrderQuery,
                    new { PizzaOrderInoviaceNo = invoiceNo }
                );

            var lst = _dapperService.Query<PizzaOrderInvoiceDetailModel>
                (
                    PizzaQuery.PizzaOrderDetailQuery,
                    new { PizzaOrderInoviaceNo = invoiceNo }
                );

            var model = new PizzaOrderInvoiceResponse
            {
                Order = item,
                OrderDetail = lst,
            };
            return Ok(model);
        }
    }
}
