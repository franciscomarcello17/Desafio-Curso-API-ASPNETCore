using System.Text.Json;
using MultiSearchAPI.Models;

namespace MultiSearchAPI.Services
{
    public class SalesOrderService
    {
        private readonly string _filePath = Path.Combine("wwwroot", "data", "sales_orders.json");

        public List<SalesOrder> GetSalesOrders()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<SalesOrder>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<SalesOrder>>(json) ?? new List<SalesOrder>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar pedidos de venda: {ex.Message}");
                return new List<SalesOrder>();
            }
        }
    }
}
