using Suitability.Application.Features.EvaluateSuitability;

namespace Suitability.UnitTests;

public class EvaluateSuitabilityTests
{
    [Fact]
    public async Task Deve_Classificar_Perfil_Arrojado()
    {
        var repo = new FakeRepo();
        var handler = new EvaluateSuitabilityHandler(repo);
        var result = await handler.Handle(new EvaluateSuitabilityRequest(24,10000,"crescimento","longo","media",8,true), default);
        Assert.Equal("ARROJADO", result.Perfil);
    }

    private sealed class FakeRepo : Suitability.Application.Interfaces.IProdutoRepository
    {
        public Task<IReadOnlyList<string>> ObterProdutosPermitidosAsync(int score, CancellationToken ct) => Task.FromResult((IReadOnlyList<string>)new List<string>{"CDB"});
    }
}
