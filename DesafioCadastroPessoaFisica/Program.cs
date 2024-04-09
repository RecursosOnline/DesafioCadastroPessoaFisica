using System.Runtime.InteropServices;
using DesafioCadastroPessoaFisica.Endpoints.PessoaFisica;
using DesafioCadastroPessoaFisica.Infraestructure;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Cadastro Pessoa Fisica", Version = "v1" });
    //options.SwaggerDoc("Desafio Cadastro Pessoa Fisica", new OpenApiInfo(){ Title = "Titulo API"});
});
var IsRunningOnDocker = RuntimeInformation.IsOSPlatform(OSPlatform.Linux) &&
                        Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
Console.Write($"IsRunningOnDocker: {IsRunningOnDocker}");
var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    //.AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddDbContext<DesafioCadastroPessoaFisicaDbContext>(options =>
{
    var x = config.GetConnectionString(!IsRunningOnDocker
        ? "DesafioCadastroPessoaFisica"
        : "DesafioCadastroPessoaFisicaDockerCompose");
    Console.WriteLine(x);
    options.UseSqlServer(config.GetConnectionString(!IsRunningOnDocker ? "DesafioCadastroPessoaFisica":"DesafioCadastroPessoaFisicaDockerCompose"));
});
builder.Services.AddControllers(options => { options.UseNamespaceRouteToken(); });


builder.Services.AddScoped<IValidator<PessoaFisica>, PessoaFisicaValidator>();

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    var dbContext = scoped.ServiceProvider.GetRequiredService<DesafioCadastroPessoaFisicaDbContext>();

    new DbInitialiser(dbContext).Run();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
app.UseHttpsRedirection();

app.Run();