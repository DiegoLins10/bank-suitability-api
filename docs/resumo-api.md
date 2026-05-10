# Resumo da Implementação da API

## Visão geral
A solução foi construída com .NET 10 e ASP.NET Core Minimal APIs para simular um motor de suitability bancária com foco em simplicidade, desempenho e separação de responsabilidades.

## Estrutura da solução
- `src/Suitability.Api`: configuração da API, endpoints e middleware
- `src/Suitability.Application`: regras de aplicação e features em Vertical Slice
- `src/Suitability.Domain`: entidades e enums de domínio
- `src/Suitability.Infrastructure`: persistência com EF Core InMemory e repositórios
- `tests/Suitability.UnitTests`: testes unitários
- `tests/Suitability.IntegrationTests`: testes de integração

## Endpoints implementados
- `POST /api/suitability/evaluate`
  - Avalia o perfil do investidor com base em experiência, renda, tolerância a risco, prazo e reserva de emergência.
  - Retorna score, perfil e lista de produtos permitidos.
- `POST /api/suitability/validate-product`
  - Valida se um produto pode ser ofertado ao cliente.
  - Retorna status de permissão, motivo e código de compliance.
- `GET /health`
  - Endpoint de health check para monitoramento básico.

## Regras e comportamento
- Classificação de perfil por score:
  - `0-39`: `CONSERVADOR`
  - `40-69`: `MODERADO`
  - `70-100`: `ARROJADO`
- Registro de auditoria de compliance em persistência InMemory.

## Recursos técnicos aplicados
- Entity Framework Core com `UseInMemoryDatabase()`
- `IMemoryCache` habilitado para evolução de cache por abstração
- FluentValidation para validação de requests
- JWT Bearer Authentication e autorização
- Swagger/OpenAPI para documentação
- Rate Limiting para proteção de consumo
- Serilog para logs estruturados
- OpenTelemetry para tracing
- Health Checks

## Qualidade
- Build da solução validado com sucesso
- Testes unitários e de integração executados com sucesso
