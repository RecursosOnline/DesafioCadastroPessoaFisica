using DesafioCadastroPessoaFisica.Infraestructe;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DesafioCadastroPessoaFisicaTest;

public class DatabaseFixture : IDisposable
{
    public readonly DesafioCadastroPessoaFisicaDbContext _DbContext;

    public DatabaseFixture(DesafioCadastroPessoaFisicaDbContext dbContext)
    {
        _DbContext = dbContext;
    }
    // public void ConfigureServices(IServiceCollection services)
    // {
    //     var config = new ConfigurationBuilder()
    //         .SetBasePath(AppContext.BaseDirectory)
    //         .AddJsonFile("appsettings.json")
    //         .Build();
    //     
    //     services.AddDbContext<DesafioCadastroPessoaFisicaDbContext>(options =>
    //     {
    //         options.UseSqlServer(config.GetConnectionString("DesafioCadastroPessoaFisica"));
    //     });
    //     
    // }
    public void Dispose()
    {
        _DbContext.Dispose();
    }
    
}
[CollectionDefinition("Database collection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
