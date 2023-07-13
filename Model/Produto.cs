using System.Text.Json.Serialization;

namespace MinimalWebApi.Model
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public DateTime DataProduto { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int CategoriaId { get; set; }

        [JsonIgnore]
        public Categoria? Categoria { get; set; }
    }
}
