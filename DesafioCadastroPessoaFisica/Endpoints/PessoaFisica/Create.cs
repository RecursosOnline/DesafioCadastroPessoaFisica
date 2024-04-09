using System.Data;
using Ardalis.ApiEndpoints;
using DesafioCadastroPessoaFisica.Infraestructure;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace DesafioCadastroPessoaFisica.Endpoints.PessoaFisica;

public class Create: EndpointBaseAsync
    .WithRequest<CreatePessoaFisicaCommand>
    .WithResult<CreatePessoaFisicaResponse>
{
    private readonly ILogger<Create> _logger;
    private readonly DesafioCadastroPessoaFisicaDbContext _dbContext;
    private readonly IValidator<Infraestructure.PessoaFisica> _validator;

    public Create(ILogger<Create> _logger, DesafioCadastroPessoaFisicaDbContext dbContext,  IValidator<Infraestructure.PessoaFisica> validator)
    {
        this._logger = _logger;
        _dbContext = dbContext;
        _validator = validator;
    }
    [HttpPost("/PessoaFisica")]
    [SwaggerOperation(
        Summary = "Create a new PessoaFisica",
        Description = "Create a new PessoaFisica",
        OperationId = "PessoaFisica_Create",
        Tags = new[] { "PessoaFisica" })
    ]
    public override async Task<CreatePessoaFisicaResponse> HandleAsync([FromBody]CreatePessoaFisicaCommand request, CancellationToken cancellationToken = new CancellationToken())
    {
        var pessoaFisica = new Infraestructure.PessoaFisica
        {
            NomeCompleto = request.NomeCompleto,
            CPF= request.CPF,
            DataDeNascimento = request.DataDeNascimento,
            ValorDaRenda = request.ValorDaRenda,
        };
        var validationResult = _validator.Validate(pessoaFisica);
        if (!validationResult.IsValid) return await Task.FromResult(new CreatePessoaFisicaResponse(Guid.Empty));

        var pessoaFisicaExistente = await _dbContext
            .Pessoas
            .SingleOrDefaultAsync(x => x.CPF == pessoaFisica.CPF);
        if (pessoaFisicaExistente is not null)
        {
            return await Task.FromResult(new CreatePessoaFisicaResponse(pessoaFisicaExistente.PessoaFisicaId));
        }
        pessoaFisica.PessoaFisicaId = Guid.NewGuid();
        _dbContext.Pessoas.Add(pessoaFisica);
        await _dbContext.SaveChangesAsync();
        return await Task.FromResult(new CreatePessoaFisicaResponse(pessoaFisica.PessoaFisicaId));
    }
}
/*
 *
 *  - Nome completo
 *  - Data de nascimento
 *  - Valor da renda
 *  - CPF
 */
public record CreatePessoaFisicaCommand(string NomeCompleto, DateOnly DataDeNascimento, decimal ValorDaRenda, string CPF);
public record CreatePessoaFisicaResponse(Guid PessoaFisicaId);

public class PessoaFisicaValidator : AbstractValidator<Infraestructure.PessoaFisica>
{
    public PessoaFisicaValidator()
    {
        RuleFor(x => x.ValorDaRenda)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.DataDeNascimento)
            .NotNull()
            .NotEmpty();
        RuleFor(x => x.NomeCompleto)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(150);
        RuleFor(x => x.CPF).Custom((cpf, context) =>
        {
            
            static bool VerificarTodosValoresSaoIguais(ref Span<int> input)
            {
                for (var i = 1; i < 11; i++)
                {
                    if (input[i] != input[0])
                    {
                        return false;
                    }
                }

                return true;
            }
            if (string.IsNullOrWhiteSpace(cpf))
            {
                context.AddFailure($"'{context.DisplayName}' não pode ser nulo ou vazio.");
                return;
            }
                
            Span<int> cpfArray = stackalloc int[11];
            var count = 0;
            foreach (var c in cpf)
            {
                if (!char.IsDigit(c))
                {
                    if(c == '.' || c == '-')
                        continue;
                    context.AddFailure($"'{context.DisplayName}' tem que ser numérico.");
                    return;
                }
            
            
                if (char.IsDigit(c))
                {
                    if (count > 10)
                    {
                        context.AddFailure($"'{context.DisplayName}' deve possuir 11 caracteres. Foram informados " + cpf.Length);
                        return;
                    }
            
            
                    cpfArray[count] = c - '0';
                    count++;
                }
            }
            
            if (count != 11)
            {
                context.AddFailure($"'{context.DisplayName}' deve possuir 11 caracteres. Foram informados " + cpf.Length);
                return;
            }
            if (VerificarTodosValoresSaoIguais(ref cpfArray))
            {
                context.AddFailure($"'{context.DisplayName}' Não pode conter todos os dígitos iguais.");
                return;
            }
            
            var totalDigitoI = 0;
            var totalDigitoII = 0;
            int modI;
            int modII;
            
            for (var posicao = 0; posicao < cpfArray.Length - 2; posicao++)
            {
                totalDigitoI += cpfArray[posicao] * (10 - posicao);
                totalDigitoII += cpfArray[posicao] * (11 - posicao);
            }
            
            modI = totalDigitoI % 11;
            if (modI < 2) { modI = 0; }
            else { modI = 11 - modI; }
            
            if (cpfArray[9] != modI)
            {
                context.AddFailure($"'{context.DisplayName}' Inválido.");
                return;
            }
            
            totalDigitoII += modI * 2;
            
            modII = totalDigitoII % 11;
            if (modII < 2) { modII = 0; }
            else { modII = 11 - modII; }
            
            return;
        });
    }
}

