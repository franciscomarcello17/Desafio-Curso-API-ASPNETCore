using System.Text.Json;
using MultiSearchAPI.Models;

namespace MultiSearchAPI.Services
{
    public class PurchaseOrderService
    {
        private readonly string _filePath = Path.Combine("wwwroot", "data", "purchase_orders.json");

        public List<PurchaseOrder> GetPurchaseOrders()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<PurchaseOrder>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<PurchaseOrder>>(json) ?? new List<PurchaseOrder>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar pedidos de compra: {ex.Message}");
                return new List<PurchaseOrder>();
            }
        }
    }
}
