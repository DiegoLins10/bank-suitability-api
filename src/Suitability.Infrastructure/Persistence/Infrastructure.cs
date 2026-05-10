using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Suitability.Application.Interfaces;
using Suitability.Domain.Entities;

namespace Suitability.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<SuitabilityDbContext>(o => o.UseInMemoryDatabase("suitability-db"));
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IComplianceRepository, ComplianceRepository>();
        return services;
    }
}

public class SuitabilityDbContext(DbContextOptions<SuitabilityDbContext> options) : DbContext(options)
{
    public DbSet<ProdutoFinanceiro> Produtos => Set<ProdutoFinanceiro>();
    public DbSet<ComplianceLog> ComplianceLogs => Set<ComplianceLog>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProdutoFinanceiro>().HasData(
            new ProdutoFinanceiro { Id = 1, Nome = "CDB", RiscoMinimo = 0 },
            new ProdutoFinanceiro { Id = 2, Nome = "TESOURO_IPCA", RiscoMinimo = 20 },
            new ProdutoFinanceiro { Id = 3, Nome = "FUNDO_MULTIMERCADO", RiscoMinimo = 40 },
            new ProdutoFinanceiro { Id = 4, Nome = "COE_ALAVANCADO", RiscoMinimo = 80 });
    }
}
public class ProdutoRepository(SuitabilityDbContext db) : IProdutoRepository
{
    public async Task<IReadOnlyList<string>> ObterProdutosPermitidosAsync(int score, CancellationToken ct) =>
        await db.Produtos.Where(p => p.RiscoMinimo <= score).OrderBy(p => p.RiscoMinimo).Select(p => p.Nome).ToListAsync(ct);
}
public class ComplianceRepository(SuitabilityDbContext db) : IComplianceRepository
{
    public async Task RegistrarAsync(int clienteId, string codigo, string mensagem, CancellationToken ct)
    {
        db.ComplianceLogs.Add(new ComplianceLog { ClienteId = clienteId, Codigo = codigo, Mensagem = mensagem });
        await db.SaveChangesAsync(ct);
    }
}
