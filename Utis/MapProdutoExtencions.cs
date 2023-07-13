using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalWebApi.ContextDb;
using MinimalWebApi.Model;

namespace MinimalWebApi.Utis
{
    public static class MapProdutoExtencions
    {
        public static void MapProduto(this WebApplication app)
        {
            app.MapPost("/produto", async ([FromBody] Produto produto,
                [FromServices] MinimalApiContext db) =>
            {
               await db.Produtos.AddAsync(produto);
               await db.SaveChangesAsync();

                return Results.Created($"/produdo/{produto.Id}", produto); 
            });

            app.MapGet("/produto", async ([FromServices] MinimalApiContext db) =>
            {
                var produtos = await db.Produtos.AsNoTracking().ToListAsync();
                if (produtos is null)
                    return Results.NotFound();
                return Results.Ok(produtos);
            });

            app.MapGet("/produto/{id:int}", async ([FromRoute] int id, 
                [FromServices] MinimalApiContext db) =>
            {

                var produto = await db.Produtos.FirstOrDefaultAsync(x => x.Id == id);

                if(produto is null)
                    return Results.NotFound();

                return Results.Ok(produto); 
            });

            app.MapPut("/produto/{id:int}", async ([FromRoute] int id, 
                [FromBody] Produto produto,
                [FromServices] MinimalApiContext db) =>
            {
                if (id != produto.Id)
                    return Results.BadRequest();
                var produtoDb = await db.Produtos.FirstOrDefaultAsync(x => x.Id == id);

                if (produtoDb is null)
                    return Results.NotFound();

                produtoDb.Nome = produto.Nome;
                produtoDb.QuantidadeEmEstoque= produto.QuantidadeEmEstoque;
                produtoDb.DataProduto = produto.DataProduto;
                produtoDb.Preco = produto.Preco;
                produtoDb.CategoriaId= produto.CategoriaId;

                db.Produtos.Update(produtoDb); 
                await db.SaveChangesAsync();

                return Results.Ok(produtoDb); 
            });

            app.MapDelete("/produtos/{id:int}", async ([FromRoute] int id, 
                [FromServices] MinimalApiContext db) =>
            {
                var produtoDb = await db.Produtos.FirstOrDefaultAsync( x => x.Id == id);
                if (produtoDb is null)
                    return Results.NotFound();
                db.Produtos.Remove(produtoDb);

                await db.SaveChangesAsync();

                return Results.Ok(produtoDb);   
            }); 
        }
    }
}
