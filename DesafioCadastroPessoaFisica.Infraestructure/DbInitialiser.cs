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
        //_context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }
}