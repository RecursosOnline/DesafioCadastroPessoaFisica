using System.Diagnostics;
using DesafioCadastroPessoaFisica.Infraestructe;
using DesafioCadastroPessoaFisica.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

namespace DesafioCadastroPessoaFisicaTest;
public class StartupOld2
{
    public static int Counter { get; set; }

    public StartupOld2() => Counter++;

    public void ConfigureHost(IHostBuilder hostBuilder) =>
        hostBuilder.ConfigureAppConfiguration(lb => lb.AddJsonFile("appsettings.json", false, true));

    public void ConfigureServices(IServiceCollection services) =>
        services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Debug).AddXunitOutput());
  

    public void Configure(IServiceProvider provider, ITestOutputHelperAccessor accessor)
    {
        Assert.NotNull(accessor);

        var listener = new ActivityListener();

        listener.ShouldListenTo += _ => true;
        listener.Sample += delegate { return ActivitySamplingResult.AllDataAndRecorded; };

        ActivitySource.AddActivityListener(listener);
    }
}
public class StartupOld
{
    //private MsSqlContainer _msSqlContainer;

    public StartupOld()
    {
        // _msSqlContainer = new MsSqlBuilder()
        //     .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        //     .Build();
        // StartMsSqlContainer().GetAwaiter().GetResult();
    }

    // private async Task StartMsSqlContainer()
    // {
    //     await _msSqlContainer
    //         .StartAsync();
    // }

    public async void ConfigureServices(IServiceCollection services)
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
        // services.AddDbContext<DesafioCadastroPessoaFisicaDbContext>(options =>
        // {
        //     
        //     options.UseSqlServer(new Lazy<string>(()=> _msSqlContainer.GetConnectionString()).Value);
        // });

    }


}