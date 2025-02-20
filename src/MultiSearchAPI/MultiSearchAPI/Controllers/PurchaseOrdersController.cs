using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;

namespace MultiSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseOrdersController : ControllerBase
    {
        private readonly PurchaseOrderService _purchaseOrderService;

        public PurchaseOrdersController()
        {
            _purchaseOrderService = new PurchaseOrderService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<PurchaseOrder>> Get()
        {
            var purchaseOrders = _purchaseOrderService.GetPurchaseOrders();
            return Ok(purchaseOrders);
        }
    }
}
