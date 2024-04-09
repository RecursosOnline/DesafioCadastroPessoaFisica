namespace DesafioCadastroPessoaFisicaTest;

[Collection("Database collection")]
public class DesafioCadastroPessoaFisicaDbContextTestDocker
{
    DatabaseFixture _fixture;
    
    public DesafioCadastroPessoaFisicaDbContextTestDocker(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }
    
    [Fact]
    public void VerificaSeConexaoEstaDisponivel()
    {
        Assert.NotNull(_fixture._DbContext);
    }
}