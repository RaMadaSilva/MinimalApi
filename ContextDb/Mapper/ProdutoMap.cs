using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinimalWebApi.Model;

namespace MinimalWebApi.ContextDb.Mapper
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable(nameof(Produto)); 
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnName("Nome")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property<decimal>(x => x.Preco)
                .IsRequired()
                .HasColumnName("Preco")
                .HasPrecision(10, 2); 

            builder.Property<int>(x => x.QuantidadeEmEstoque)
                .IsRequired()
                .HasColumnName("Quantidade");

            builder.Property<DateTime>(x => x.DataProduto)
                .IsRequired()
                .HasColumnName("DataProduto");

            //Relacionamento
            builder.HasOne(x => x.Categoria)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
