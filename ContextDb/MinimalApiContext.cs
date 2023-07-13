using Microsoft.EntityFrameworkCore;
using MinimalWebApi.ContextDb.Mapper;
using MinimalWebApi.Model;

namespace MinimalWebApi.ContextDb; 

public class MinimalApiContext : DbContext
{
    public MinimalApiContext(DbContextOptions<MinimalApiContext> options): base(options)
    {
    }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration(new CategoriaMap());
        mb.ApplyConfiguration(new ProdutoMap()); 
    }
}
