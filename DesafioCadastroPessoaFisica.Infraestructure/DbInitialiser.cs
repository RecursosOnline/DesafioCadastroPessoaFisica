using Polly;
using Polly.Retry;

namespace DesafioCadastroPessoaFisica.Infraestructure;

public class DbInitialiser
{
    private readonly DesafioCadastroPessoaFisicaDbContext _context;

    public DbInitialiser(DesafioCadastroPessoaFisicaDbContext context)
    {
        _context = context;
    }

    public void Run()
    {
        ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
            .AddRetry(new RetryStrategyOptions()
            {
                Delay = TimeSpan.FromSeconds(20),
               // MaxRetryAttempts = 10,
            })
            .Build();
        pipeline.Execute(() =>
            {
                try
                {
                    _context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        ); 
    }
}