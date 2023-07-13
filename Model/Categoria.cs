using System.Text.Json.Serialization;

namespace MinimalWebApi.Model
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new List<Produto>();
        }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Produto> Produtos { get; set; }
    }
}
