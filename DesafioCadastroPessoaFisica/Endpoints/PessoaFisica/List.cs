using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using DesafioCadastroPessoaFisica.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioCadastroPessoaFisica.Endpoints.PessoaFisica;

public class List : EndpointBaseAsync
    .WithoutRequest
    .WithResult<List<PessoaFisicaResponse>>
{
    private readonly ILogger<Create> _logger;
    private readonly DesafioCadastroPessoaFisicaDbContext _dbContext;

    public List(ILogger<Create> logger, DesafioCadastroPessoaFisicaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [HttpGet("/PessoaFisica")]
    [SwaggerOperation(
        Summary = "List of PessoaFisica",
        Description = "Return a list of PessoaFisica",
        OperationId = "PessoaFisica_List",
        Tags = new[] { "PessoaFisica" })
    ]
    public override Task<List<PessoaFisicaResponse>> HandleAsync(
        CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(_dbContext
            .Pessoas
            .ToList()    
            .Select(x => new PessoaFisicaResponse(
                x.PessoaFisicaId,
                x.NomeCompleto,
                x.DataDeNascimento,
                x.ValorDaRenda,
                x.CPF
            ))
            .ToList());
    }
}

public record PessoaFisicaResponse(
    Guid pessoaFisicaId,
    [Required]string NomeCompleto,
    DateOnly DataDeNascimento,
    decimal ValorDaRenda,
    string CPF);