using Microsoft.AspNetCore.Mvc;  

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

// TABELA
List<Folha> folhas = new List<Folha>();

// C - CREATE 
app.MapPost("/api/folha/cadastrar", 
    ([FromBody] Folha? folha,
    [FromServices] AppDataContext ctx) =>
{
    if (folha is null)
    {
        return Results.BadRequest("Erro, não pode ser nulo");
    }

    if (folha.Nome == "")
    {
        return Results.BadRequest("O nome não pode ser vazio");
    }

    if (folha.HorasTrabalhadas == null)
    {
        return Results.BadRequest("Insira horas trabalhadas");
    }

    if (folha.ValorHora == null)
    {
        return Results.BadRequest("Insira valor horas");
    }


    folha.SalarioBruto = folha.HorasTrabalhadas * folha.ValorHora;



    if (folha.SalarioBruto > 1903.99 && folha.SalarioBruto < 2826.65)
    {
        folha.ImpostoRenda = (folha.SalarioBruto * 0.075) - 142.80;
    }

    if (folha.SalarioBruto > 2826.66 && folha.SalarioBruto < 3751.05)
    {
        folha.ImpostoRenda = (folha.SalarioBruto * 0.15) - 345.80;
    }

    if (folha.SalarioBruto > 3751.06 && folha.SalarioBruto < 4664.68)
    {
        folha.ImpostoRenda = (folha.SalarioBruto * 0.225) - 636.13;
    }

    if (folha.SalarioBruto > 4664.68)
    {
        folha.ImpostoRenda = (folha.SalarioBruto * 0.275) - 869.36;
    }

    // INSS
    if (folha.SalarioBruto < 1693.72)
    {
        folha.Inss = folha.SalarioBruto * 0.08;
    }

    if (folha.SalarioBruto > 1693.73 && folha.SalarioBruto < 2822.90)
    {
        folha.Inss = folha.SalarioBruto * 0.09;
    }

    if (folha.SalarioBruto > 2822.91 && folha.SalarioBruto < 5645.81)
    {
        folha.Inss = folha.SalarioBruto * 0.11;
    }

    if (folha.SalarioBruto > 5645.81)
    {
        folha.Inss = 621.03;
    }

    folha.Fgts = folha.SalarioBruto * 0.08;

    folha.SalarioLiquido = folha.SalarioBruto - folha.ImpostoRenda - folha.Inss;
    

    Folha? folhaEncontrado = ctx.Folhas.FirstOrDefault(x => x.Nome == folha.Nome);
    if (folhaEncontrado is not null)
    {
        return Results.BadRequest("Esse cadastro ja existe!");
    }

    ctx.Folhas.Add(folha);
    ctx.SaveChanges();
    return Results.Created("", folha);
});



app.MapGet("/api/folha/listar", (
    [FromServices] AppDataContext ctx) =>
{
    if (ctx.Folhas.Count() == 0)
    {
        return Results.BadRequest("A lista está vazia");
    }
    return Results.Ok(ctx.Folhas.ToList());
});


app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", 
    ([FromRoute] string cpf,
    [FromRoute] int mes,
    [FromRoute] int ano,
    [FromServices] AppDataContext ctx) =>
{
    
    Folha? folhaEncontrado = ctx.Folhas.FirstOrDefault(x => x.Cpf == cpf);
    if (folhaEncontrado is null)
    {
        return Results.NotFound("Folha não encontrado!");
    }
    return Results.Ok(folhaEncontrado);    
});

app.MapDelete("api/folha/remover/{cpf}/{mes}/{ano}", 
    ([FromRoute] string cpf,
    [FromRoute] int mes,
    [FromRoute] int ano,
    [FromServices] AppDataContext ctx) =>
{
    
    Folha? folhaEncontrado = ctx.Folhas.FirstOrDefault(x => x.Cpf == cpf);
    if (folhaEncontrado is null)
    {
        return Results.NotFound("Folha não encontrado!");
    }
    ctx.Folhas.Remove(folhaEncontrado);
    ctx.SaveChanges();
    return Results.Ok(folhaEncontrado);    
});


app.MapGet("/api/folha/total-liquido", (
    [FromBody] Folha? folha,
    [FromServices] AppDataContext ctx) =>
{
    if (ctx.Folhas.Count() == 0)
    {
        return Results.BadRequest("A lista está vazia");
    }

    
    return Results.Ok(folha.SalarioLiquido);
});


app.Run();
