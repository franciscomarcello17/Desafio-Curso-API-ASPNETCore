using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;
using System.Collections.Generic;
using System.Linq;

namespace MultiSearchAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly EquipmentService _equipmentService;
        private readonly MaterialService _materialService;
        private readonly SalesOrderService _salesOrderService;
        private readonly PurchaseOrderService _purchaseOrderService;
        private readonly WorkforceService _workforceService;

        public SearchController()
        {
            _equipmentService = new EquipmentService();
            _materialService = new MaterialService();
            _salesOrderService = new SalesOrderService();
            _purchaseOrderService = new PurchaseOrderService();
            _workforceService = new WorkforceService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("A consulta de pesquisa não pode estar vazia.");
            }

            var equipamentos = (_equipmentService.GetEquipments() ?? new List<Equipment>())
                .Where(e => e.EquipmentName.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .Select(e => (object)new { Category = "Equipamento", Id = e.EquipmentID, Name = e.EquipmentName, Details = "" })
                .ToList();

            var materiais = (_materialService.GetMaterials() ?? new List<Material>())
                .Where(m => m.MaterialName.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .Select(m => (object)new { Category = "Material", Id = m.MaterialID, Name = m.MaterialName, Details = "" })
                .ToList();

            var pedidosCompra = (_purchaseOrderService.GetPurchaseOrders() ?? new List<PurchaseOrder>())
                .Where(pc => pc.MaterialName.Contains(query, System.StringComparison.OrdinalIgnoreCase) || pc.Supplier.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .Select(pc => (object)new { Category = "Pedido de Compra", Id = pc.PurchaseOrderID, Name = pc.MaterialName, Details = $"Fornecedor: {pc.Supplier}, Quantidade: {pc.Quantity}, Custo: R$ {pc.TotalCost:F2}" })
                .ToList();

            var pedidosVenda = (_salesOrderService.GetSalesOrders() ?? new List<SalesOrder>())
                .Where(pv => pv.MaterialName.Contains(query, System.StringComparison.OrdinalIgnoreCase) || pv.Customer.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .Select(pv => (object)new { Category = "Pedido de Venda", Id = pv.SalesOrderID, Name = pv.MaterialName, Details = $"Cliente: {pv.Customer}, Quantidade: {pv.Quantity}, Valor: R$ {pv.TotalValue:F2}" })
                .ToList();

            var maoDeObra = (_workforceService.GetWorkforce() ?? new List<Workforce>())
                .Where(m => m.Name.Contains(query, System.StringComparison.OrdinalIgnoreCase))
                .Select(m => (object)new { Category = "Mão de Obra", Id = m.WorkforceID, Name = m.Name, Details = $"Turno: {m.Shift}" })
                .ToList();

            var results = new List<object>();
            results.AddRange(equipamentos);
            results.AddRange(materiais);
            results.AddRange(pedidosCompra);
            results.AddRange(pedidosVenda);
            results.AddRange(maoDeObra);

            return Ok(results);
        }
    }
}
