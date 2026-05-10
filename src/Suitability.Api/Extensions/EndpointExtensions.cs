using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Suitability.Application.Features.EvaluateSuitability;
using Suitability.Application.Features.ValidateProduct;

namespace Suitability.Api.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapSuitabilityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/suitability").RequireRateLimiting("fixed");

        group.MapPost("/evaluate", async Task<Results<Ok<EvaluateSuitabilityResponse>, ValidationProblem>> (EvaluateSuitabilityRequest req, IValidator<EvaluateSuitabilityRequest> validator, EvaluateSuitabilityHandler handler, CancellationToken ct) =>
        {
            var validation = await validator.ValidateAsync(req, ct);
            if (!validation.IsValid) return TypedResults.ValidationProblem(validation.ToDictionary());
            return TypedResults.Ok(await handler.Handle(req, ct));
        });

        group.MapPost("/validate-product", async Task<Results<Ok<ValidateProductResponse>, ValidationProblem>> (ValidateProductRequest req, IValidator<ValidateProductRequest> validator, ValidateProductHandler handler, CancellationToken ct) =>
        {
            var validation = await validator.ValidateAsync(req, ct);
            if (!validation.IsValid) return TypedResults.ValidationProblem(validation.ToDictionary());
            return TypedResults.Ok(await handler.Handle(req, ct));
        }).RequireAuthorization();

        return app;
    }
}
