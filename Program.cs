using Microsoft.EntityFrameworkCore;
using MinimalWebApi.ContextDb;
using MinimalWebApi.Utis;

var builder = WebApplication.CreateBuilder(args);

var connetion = builder.Configuration.GetConnectionString("DafaultConnetion");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MinimalApiContext>(options=>options.UseSqlServer(connetion)); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCategoria();
app.MapProduto(); 

app.Run();
