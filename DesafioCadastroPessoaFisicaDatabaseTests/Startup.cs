using DesafioCadastroPessoaFisica.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace DesafioCadastroPessoaFisicaDatabaseTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        
        services.AddDbContext<DesafioCadastroPessoaFisicaDbContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DesafioCadastroPessoaFisica"));
        });
        
        services.AddLogging(l => l.AddXunitOutput());
    }
}