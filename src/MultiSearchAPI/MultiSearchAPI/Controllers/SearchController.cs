using Microsoft.AspNetCore.Mvc;
using MultiSearchAPI.Models;
using MultiSearchAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

            var equipamentos = FilterByQuery(_equipmentService.GetEquipments() ?? new List<Equipment>(), query)
                .Select(e => new { Category = "Equipamento", Id = e.EquipmentID, Name = e.EquipmentName, Details = "" })
                .ToList();

            var materiais = FilterByQuery(_materialService.GetMaterials() ?? new List<Material>(), query)
                .Select(m => new { Category = "Material", Id = m.MaterialID, Name = m.MaterialName, Details = "" })
                .ToList();

            var pedidosCompra = FilterByQuery(_purchaseOrderService.GetPurchaseOrders() ?? new List<PurchaseOrder>(), query)
                .Select(pc => new { Category = "Pedido de Compra", Id = pc.PurchaseOrderID, Name = pc.MaterialName, Details = $"Fornecedor: {pc.Supplier}, Quantidade: {pc.Quantity}, Custo: R$ {pc.TotalCost:F2}" })
                .ToList();

            var pedidosVenda = FilterByQuery(_salesOrderService.GetSalesOrders() ?? new List<SalesOrder>(), query)
                .Select(pv => new { Category = "Pedido de Venda", Id = pv.SalesOrderID, Name = pv.MaterialName, Details = $"Cliente: {pv.Customer}, Quantidade: {pv.Quantity}, Valor: R$ {pv.TotalValue:F2}" })
                .ToList();

            var maoDeObra = FilterByQuery(_workforceService.GetWorkforce() ?? new List<Workforce>(), query)
                .Select(m => new { Category = "Mão de Obra", Id = m.WorkforceID, Name = m.Name, Details = $"Turno: {m.Shift}" })
                .ToList();

            var results = new List<object>();
            results.AddRange(equipamentos);
            results.AddRange(materiais);
            results.AddRange(pedidosCompra);
            results.AddRange(pedidosVenda);
            results.AddRange(maoDeObra);

            return Ok(results);
        }

        private static IEnumerable<T> FilterByQuery<T>(IEnumerable<T> list, string query)
        {
            return list.Where(item =>
            {
                foreach (PropertyInfo prop in typeof(T).GetProperties())
                {
                    var value = prop.GetValue(item)?.ToString();
                    if (!string.IsNullOrEmpty(value) && value.Contains(query, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }
                return false;
            });
        }
    }
}
