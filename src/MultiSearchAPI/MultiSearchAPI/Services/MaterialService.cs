using System.Text.Json;
using MultiSearchAPI.Models;

namespace MultiSearchAPI.Services
{
    public class MaterialService
    {
        private readonly string _filePath = Path.Combine("wwwroot", "data", "materials.json");

        public List<Material> GetMaterials()
        {
            try
            {
                if (!File.Exists(_filePath)) return new List<Material>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Material>>(json) ?? new List<Material>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar materiais: {ex.Message}");
                return new List<Material>();
            }
        }
    }
}
