using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioCadastroPessoaFisica.Endpoints;

public class PessoaFisicaEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithResult<string>

{
    private readonly ILogger<PessoaFisicaEndpoint> _logger;

    public PessoaFisicaEndpoint(ILogger<PessoaFisicaEndpoint> logger)
    {
        _logger = logger;
    }
    [HttpGet("/PessoaFisica")]
    [SwaggerOperation(
        Summary = "Get PessoaFisica",
        OperationId = "PessoaFisica_Get",
        Tags = new[] { "PessoaFisica" })
    ]
    public override Task<string> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation("Localizando PessoaFisica");
        return Task.FromResult("Ok deu tudo certo");

    }
}
