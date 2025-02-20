using System.Text.Json;
using MultiSearchAPI.Models;

namespace MultiSearchAPI.Services
{
    public class EquipmentService
    {
        private readonly string _filePath = Path.Combine("wwwroot", "data", "equipments.json");

        public List<Equipment> GetEquipments()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<Equipment>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Equipment>>(json) ?? new List<Equipment>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar equipamentos: {ex.Message}");
                return new List<Equipment>();
            }
        }
    }
}
