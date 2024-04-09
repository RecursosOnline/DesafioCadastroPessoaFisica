using Ardalis.ApiEndpoints;
using DesafioCadastroPessoaFisica.Infraestructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioCadastroPessoaFisica.Endpoints.PessoaFisica;

public class Update: EndpointBaseAsync
    .WithRequest<UpdatePessoaFisicaCommand>
    .WithResult<bool>
{
    private readonly ILogger<Update> _logger;
    private readonly DesafioCadastroPessoaFisicaDbContext _dbContext;
    private readonly IValidator<Infraestructure.PessoaFisica> _validator;


    public Update(ILogger<Update> logger, DesafioCadastroPessoaFisicaDbContext dbContext,  IValidator<Infraestructure.PessoaFisica> validator)
    {
        _logger = logger;
        _dbContext = dbContext;
        _validator = validator;
    }
    [HttpPut("/PessoaFisica")]
    [SwaggerOperation(
        Summary = "Update a PessoaFisica",
        Description = "Update a  PessoaFisica",
        OperationId = "PessoaFisica_Update",
        Tags = new[] { "PessoaFisica" })
    ]
    public override async Task<bool> HandleAsync(UpdatePessoaFisicaCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation("Atualizando dados de {NomeCompleto}", request.NomeCompleto);
        var pessoaFisica = new Infraestructure.PessoaFisica
        {
            NomeCompleto = request.NomeCompleto,
            CPF= request.CPF,
            DataDeNascimento = request.DataDeNascimento,
            ValorDaRenda = request.ValorDaRenda,
            PessoaFisicaId = request.pessoaFisicaId
        };
        var validationResult = _validator.Validate(pessoaFisica);
        if (!validationResult.IsValid) return await Task.FromResult(false);

        var pessoaFisicaExistente = await _dbContext
            .Pessoas
            .SingleOrDefaultAsync(x => x.PessoaFisicaId == pessoaFisica.PessoaFisicaId);
        if (pessoaFisicaExistente is null)
        {
            return await Task.FromResult(false);
        }

        pessoaFisicaExistente.NomeCompleto = pessoaFisica.NomeCompleto;
        pessoaFisicaExistente.CPF = pessoaFisica.CPF;
        pessoaFisicaExistente.ValorDaRenda = pessoaFisica.ValorDaRenda;
        pessoaFisicaExistente.DataDeNascimento = pessoaFisica.DataDeNascimento;
        _dbContext.Pessoas.Update(pessoaFisicaExistente);
        var result = await _dbContext.SaveChangesAsync();
        return await Task.FromResult(result > 0);
    }
}

public record UpdatePessoaFisicaCommand(Guid pessoaFisicaId,string NomeCompleto, DateOnly DataDeNascimento, decimal ValorDaRenda, string CPF);
