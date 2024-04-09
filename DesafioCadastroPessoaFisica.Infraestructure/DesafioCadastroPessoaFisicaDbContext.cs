using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace DesafioCadastroPessoaFisica.Infraestructure;

public class DesafioCadastroPessoaFisicaDbContext : DbContext
{
    public DbSet<PessoaFisica> Pessoas { get; set; }

    public DesafioCadastroPessoaFisicaDbContext(DbContextOptions<DesafioCadastroPessoaFisicaDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder
            .Entity<PessoaFisica>()
            .Property(p => p.ValorDaRenda)
            .HasColumnType("decimal")
            .HasPrecision(12,2);
        modelBuilder
            .Entity<PessoaFisica>()
            .HasIndex(p => p.CPF)
            .IsUnique(true);
        base.OnModelCreating(modelBuilder);
    }
}

/*
 *
 *  - Nome completo
 *  - Data de nascimento
 *  - Valor da renda
 *  - CPF
 */
public class PessoaFisica
{
    [Key]
    public Guid PessoaFisicaId { get; set; }
    [Required]
    public string NomeCompleto { get; set; } = "";
    [Required]
    public DateOnly DataDeNascimento { get; set; }
    [Required,Precision(2)]
    public Decimal ValorDaRenda { get; set; } = 0;
    [Required]
    public string CPF { get; set; } = "";
}