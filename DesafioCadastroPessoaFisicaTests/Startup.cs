// using DesafioCadastroPessoaFisica.Infraestructure;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.DependencyInjection;
// using Testcontainers.MsSql;
//
// namespace DesafioCadastroPessoaFisicaTest;

using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Xunit.DependencyInjection;
using Xunit.DependencyInjection.Logging;

public class Startup
{
    public static int Counter { get; set; }

    public Startup() => Counter++;

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


//
// public class Startup
// {
//     //private MsSqlContainer _msSqlContainer;
//
//     public Startup()
//     {
//         // _msSqlContainer = new MsSqlBuilder()
//         //     .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
//         //     .Build();
//         // StartMsSqlContainer().GetAwaiter().GetResult();
//     }
//
//     // private async Task StartMsSqlContainer()
//     // {
//     //     await _msSqlContainer
//     //         .StartAsync();
//     // }
//
//     public async void ConfigureServices(IServiceCollection services)
//     {
//         var config = new ConfigurationBuilder()
//             .SetBasePath(AppContext.BaseDirectory)
//             .AddJsonFile("appsettings.json")
//             .Build();
//         
//         services.AddDbContext<DesafioCadastroPessoaFisicaDbContext>(options =>
//         {
//             options.UseSqlServer(config.GetConnectionString("DesafioCadastroPessoaFisica"));
//         });
//         // services.AddDbContext<DesafioCadastroPessoaFisicaDbContext>(options =>
//         // {
//         //     
//         //     options.UseSqlServer(new Lazy<string>(()=> _msSqlContainer.GetConnectionString()).Value);
//         // });
//         
//     }
//
//
// }
