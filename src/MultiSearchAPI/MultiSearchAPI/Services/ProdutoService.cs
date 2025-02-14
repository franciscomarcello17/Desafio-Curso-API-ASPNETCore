using System.Text.Json;
using MultiSearchAPI.Models;

namespace MultiSearchAPI.Services
{
    public class ProdutoService
    {
        private readonly string _filePath = "produtos.json";

        public List<Produto> GetProdutos()
        {
            if (!File.Exists(_filePath)) return new List<Produto>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Produto>>(json) ?? new List<Produto>();
        }
    }
}
