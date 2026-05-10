using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace Suitability.Application.Features.EvaluateSuitability;
public sealed record EvaluateSuitabilityRequest(int Idade, decimal RendaMensal, string Objetivo, string Prazo, string Experiencia, int ToleranciaRisco, bool PossuiReservaEmergencia);
public sealed record EvaluateSuitabilityResponse(int Score, string Perfil, DateOnly Validade, IReadOnlyList<string> ProdutosPermitidos);
public sealed class EvaluateSuitabilityValidator : AbstractValidator<EvaluateSuitabilityRequest>
{
    public EvaluateSuitabilityValidator() { RuleFor(x => x.Idade).InclusiveBetween(18, 120); RuleFor(x => x.ToleranciaRisco).InclusiveBetween(0, 10); }
}
public sealed class EvaluateSuitabilityHandler
{
    private readonly Interfaces.IProdutoRepository _repo;
    public EvaluateSuitabilityHandler(Interfaces.IProdutoRepository repo) => _repo = repo;
    public async Task<EvaluateSuitabilityResponse> Handle(EvaluateSuitabilityRequest req, CancellationToken ct)
    {
        var score = Math.Min(100, (req.Experiencia.ToLower() switch { "alta" => 30, "media" => 20, _ => 10 }) + (req.RendaMensal >= 10000 ? 20 : req.RendaMensal >= 5000 ? 10 : 5) + (req.ToleranciaRisco * 3) + (req.Prazo.ToLower() == "longo" ? 15 : 5) + (req.PossuiReservaEmergencia ? 5 : 0));
        var perfil = score <= 39 ? "CONSERVADOR" : score <= 69 ? "MODERADO" : "ARROJADO";
        var produtos = await _repo.ObterProdutosPermitidosAsync(score, ct);
        return new(score, perfil, DateOnly.FromDateTime(DateTime.UtcNow.AddYears(2)), produtos);
    }
}
