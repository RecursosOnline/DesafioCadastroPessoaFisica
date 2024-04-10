using Ardalis.ApiEndpoints;
using DesafioCadastroPessoaFisica.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioCadastroPessoaFisica.Endpoints.PessoaFisica;

public class Delete: EndpointBaseAsync
    .WithRequest<DeletePessoaFisicaCommand>
    .WithResult<bool>
{
    private readonly ILogger<Delete> _logger;
    private readonly DesafioCadastroPessoaFisicaDbContext _dbContext;
    public Delete(ILogger<Delete> logger, DesafioCadastroPessoaFisicaDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    [HttpDelete("/PessoaFisica")]
    [SwaggerOperation(
        Summary = "Delete a PessoaFisica",
        Description = "Delete a PessoaFisica",
        OperationId = "PessoaFisica_Delete",
        Tags = new[] { "PessoaFisica" })
    ]
    public override async Task<bool> HandleAsync(DeletePessoaFisicaCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation("Excluindo PessoaFisicaId: {PessoaFisicaId}", request.pessoaFisicaId);
        var pessoaFisica = await _dbContext
            .Pessoas
            .SingleOrDefaultAsync(x => x.PessoaFisicaId == request.pessoaFisicaId, cancellationToken);
        if (pessoaFisica is not null)
        {
            _dbContext.Pessoas.Remove(pessoaFisica);
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }
        
        return await Task.FromResult(false);
    }
}

public record DeletePessoaFisicaCommand(Guid pessoaFisicaId);
