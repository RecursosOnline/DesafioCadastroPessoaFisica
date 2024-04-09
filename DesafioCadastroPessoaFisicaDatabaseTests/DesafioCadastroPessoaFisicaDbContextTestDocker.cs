using DesafioCadastroPessoaFisica.Infraestructure;

namespace DesafioCadastroPessoaFisicaDatabaseTests;

[Collection("Database collection")]
public class DesafioCadastroPessoaFisicaDbContextTestDocker
{
    private DesafioCadastroPessoaFisicaDbContext _dbContext;
    private readonly PessoaFisica _pessoaFisica;

    public DesafioCadastroPessoaFisicaDbContextTestDocker(DatabaseFixture fixture)
    {
        _dbContext = fixture._DbContext;
        new DbInitialiser(_dbContext).Run();
        _pessoaFisica = new()
        {
            PessoaFisicaId = Guid.NewGuid(),
            NomeCompleto = "Fabio Silva",
            CPF = "147.xxx.188-83",
            DataDeNascimento = DateOnly.Parse("21/03/1976"),
            ValorDaRenda = 16000m
        };
    }

    [Fact]
    public void CriarUsuarioRetornaSucesso()
    {
        var exists = _dbContext
            .Pessoas
            .Where(x => x.CPF == _pessoaFisica.CPF)
            .FirstOrDefault();
        if (exists is null)
        {
            _dbContext.Pessoas.Add(_pessoaFisica);
            _dbContext.SaveChanges();
        }
        Assert.True(_dbContext.Pessoas.Any());
    }

    [Fact]
    public void CriarUsuarioRetornaSucesso2()
    {
        _dbContext.Pessoas.Add(_pessoaFisica);
        _dbContext.SaveChanges();
        Assert.True(_dbContext.Pessoas.Any());
    }
}