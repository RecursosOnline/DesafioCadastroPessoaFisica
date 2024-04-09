using DesafioCadastroPessoaFisica.Infraestructe;

namespace Xunit.DependencyInjection.Test.CollectionFixture;

public class CollectionFixtureWithDependency(DesafioCadastroPessoaFisicaDbContext dependency) : IDisposable
{
    public bool IsDisposed { get; private set; }

    public DesafioCadastroPessoaFisicaDbContext Dependency { get; } = dependency;

    public void Dispose() => IsDisposed = true;
}