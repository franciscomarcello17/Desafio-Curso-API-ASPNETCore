using System.Text.Json;
using MultiSearchAPI.Models;

namespace MultiSearchAPI.Services
{
    public class WorkforceService
    {
        private readonly string _filePath = Path.Combine("wwwroot", "data", "workforce.json");

        public List<Workforce> GetWorkforce()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<Workforce>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Workforce>>(json) ?? new List<Workforce>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar mão de obra: {ex.Message}");
                return new List<Workforce>();
            }
        }
    }
}
