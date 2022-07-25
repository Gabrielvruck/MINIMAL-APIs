using Microsoft.EntityFrameworkCore;
using MinimalAPIs.Contexto;
using MinimalAPIs.Models;
//para saber mais sobre MINIMAL APIS https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0

var builder = WebApplication.CreateBuilder(args);
//AddEndpointsApiExplorer serve para abrir o navegador
builder.Services.AddEndpointsApiExplorer();
//necessario installar pacote nuget Microsoft.EntityFrameworkCore.SqlServer
builder.Services.AddDbContext<Contexto>(options => options.UseSqlServer(
        "Data Source=localhost;Initial Catalog=MINIMAL_APIS; Integrated Security = true; Encrypt = False; TrustServerCertificate = true"));

//Criar o swagger
//installar pacote nuget
//Swashbuckle.AspNetCore.Swagger
//Swashbuckle.AspNetCore.SwaggerGen
//Swashbuckle.AspNetCore.SwaggerUI

//adicionar as dependencia do swagger 
builder.Services.AddSwaggerGen();

var app = builder.Build();

//adicionar as dependencia do swagger 
app.UseSwagger();

app.MapGet("/", () => "Hello World!");

app.MapPost("AdicionaProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});

app.MapPost("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produtoExcluir = await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
    if (produtoExcluir != null)
    {
        contexto.Produto.Remove(produtoExcluir);
        await contexto.SaveChangesAsync();
    }
});

app.MapPost("ListarProdutos", async (Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
});

app.MapPost("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
});

//adicionar as dependencia do swagger 
app.UseSwaggerUI();
app.Run();
