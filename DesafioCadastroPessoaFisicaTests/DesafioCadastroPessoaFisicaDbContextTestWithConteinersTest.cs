// using DesafioCadastroPessoaFisica.Infraestructure;
// using Microsoft.EntityFrameworkCore;
// using Testcontainers.MsSql;
// using Xunit.Abstractions;
//
// namespace DesafioCadastroPessoaFisicaTest;
//
// public class DesafioCadastroPessoaFisicaDbContextTestWithConteinersTest: IAsyncLifetime
// {
//     private readonly ITestOutputHelper _output;
//     private DesafioCadastroPessoaFisicaDbContext _dbContext;
//     private MsSqlContainer _msSqlContainer;
//
//
//     public DesafioCadastroPessoaFisicaDbContextTestWithConteinersTest(ITestOutputHelper output)
//     {
//         _output = output;
//     }
//     // public DesafioCadastroPessoaFisicaDbContextTest(ITestOutputHelper output)
//     // {
//     //     _output = output;
//     // }
//     [Fact]
//     public void Test1()
//     {
//         Assert.NotNull(_dbContext);
//         var sut = new DbInitialiser(_dbContext);
//         
//         sut.Run();
//
//         _dbContext.Pessoas.Add(new()
//         {
//             PessoaFisicaId  = Guid.NewGuid(),
//             NomeCompleto = "Fabio Silva",
//             CPF = "147.xxx.188-83",
//             DataDeNascimento = DateOnly.Parse("21/03/1976"),
//             ValorDaRenda = 16000m
//         });
//         _dbContext.SaveChanges();
//         Assert.True(_dbContext.Pessoas.Any());
//
//     }
//
//     public async Task InitializeAsync()
//     {
//         _msSqlContainer = new MsSqlBuilder()
//             .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
//             .Build();
//         await _msSqlContainer.StartAsync();
//         var options = new DbContextOptionsBuilder<DesafioCadastroPessoaFisicaDbContext>()
//             .UseSqlServer(_msSqlContainer.GetConnectionString()); 
//         
//         _dbContext = new DesafioCadastroPessoaFisicaDbContext(options.Options); 
//          
//         await Task.CompletedTask;
//     }
//
//     public async Task DisposeAsync()
//     {
//         await _msSqlContainer.DisposeAsync();
//     }
// }