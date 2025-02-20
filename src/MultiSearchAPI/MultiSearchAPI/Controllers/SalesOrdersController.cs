using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;

namespace MultiSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesOrdersController : ControllerBase
    {
        private readonly SalesOrderService _salesOrderService;

        public SalesOrdersController()
        {
            _salesOrderService = new SalesOrderService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<SalesOrder>> Get()
        {
            var salesOrders = _salesOrderService.GetSalesOrders();
            return Ok(salesOrders);
        }
    }
}
