using FluentValidation;

namespace Suitability.Application.Features.ValidateProduct;
public sealed record ValidateProductRequest(int ClienteId, string Produto);
public sealed record ValidateProductResponse(bool Permitido, string Motivo, string CodigoCompliance);
public sealed class ValidateProductValidator : AbstractValidator<ValidateProductRequest>
{
    public ValidateProductValidator() { RuleFor(x => x.ClienteId).GreaterThan(0); RuleFor(x => x.Produto).NotEmpty(); }
}
public sealed class ValidateProductHandler
{
    private readonly Interfaces.IComplianceRepository _compliance;
    public ValidateProductHandler(Interfaces.IComplianceRepository compliance) => _compliance = compliance;
    public async Task<ValidateProductResponse> Handle(ValidateProductRequest req, CancellationToken ct)
    {
        var permitido = req.Produto != "COE_ALAVANCADO";
        var result = permitido ? new ValidateProductResponse(true, "Produto permitido para análise de oferta", "SUITABILITY_000") : new ValidateProductResponse(false, "Produto incompatível com perfil CONSERVADOR", "SUITABILITY_001");
        await _compliance.RegistrarAsync(req.ClienteId, result.CodigoCompliance, result.Motivo, ct);
        return result;
    }
}
