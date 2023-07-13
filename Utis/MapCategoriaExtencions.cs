using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinimalWebApi.ContextDb;
using MinimalWebApi.Model;

namespace MinimalWebApi.Utis
{
    public static class MapCategoriaExtencions
    {
        public static void MapCategoria(this WebApplication app)
        {
            app.MapGet("/", () => "Bem Vindo ao Minimal webApi do Raul Silva");

            app.MapPost("/categoria", async ([FromBody] Categoria categoria,
                [FromServices] MinimalApiContext db) =>
            {
                await db.AddAsync(categoria);
                db.SaveChanges();

                return Results.Created($"categoria/{categoria.Id}", categoria);
            });

            app.MapGet("/categoria", async ([FromServices] MinimalApiContext db) =>
            {
                var categorias = await db.Categorias.ToListAsync();

                if (categorias is null)
                    return Results.NotFound("Lista de categorias vazia");

                return Results.Ok(categorias);
            });

            app.MapGet("/categoria/{id:int}", async ([FromRoute] int id,
                [FromServices] MinimalApiContext db) =>
            {
                var cat = await db.Categorias.FirstOrDefaultAsync(x => x.Id == id);

                if (cat is null)
                    return Results.NotFound($"id {id} Invalido");
                return Results.Ok(cat);
            });

            app.MapPut("/categoria/{id:int}", async ([FromRoute] int id,
                [FromBody] Categoria categoria,
                [FromServices] MinimalApiContext db) =>
            {
                if (id != categoria.Id)
                    return Results.BadRequest();
                var categoriaBd = await db.Categorias.FirstOrDefaultAsync(x => x.Id == id);

                if (categoriaBd is null)
                    return Results.NotFound();

                categoriaBd.Nome = categoria.Nome;
                categoriaBd.Descricao = categoria.Descricao;

                db.Update(categoriaBd);
                await db.SaveChangesAsync();

                return Results.Ok(categoriaBd);
            });

            app.MapDelete("/categoria/{id:int}", async ([FromRoute] int id,
                [FromServices] MinimalApiContext bd) =>
            {
                var categoriaBd = await bd.Categorias.FirstOrDefaultAsync(x => x.Id == id);
                if (categoriaBd is null)
                    return Results.NotFound();
                bd.Categorias.Remove(categoriaBd);
                await bd.SaveChangesAsync();

                return Results.Ok(categoriaBd);

            });

        }
    }
}
