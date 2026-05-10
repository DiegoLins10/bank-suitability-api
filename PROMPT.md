Crie uma API de Suitability bancária utilizando **.NET 10** com **ASP.NET Core Minimal APIs**, seguindo as recomendações modernas da Microsoft para APIs performáticas, simples e escaláveis.

A aplicação deve simular um sistema real utilizado por bancos e corretoras para validação de perfil de investidor, regras de compliance e autorização de ofertas financeiras.

# Objetivos da API

A API deve:

* avaliar perfil do investidor;
* calcular score de risco;
* classificar cliente como:

  * CONSERVADOR
  * MODERADO
  * ARROJADO
* validar se um produto financeiro pode ser ofertado;
* retornar mensagens de compliance;
* registrar auditoria;
* possuir arquitetura enterprise moderna.

---

# Stack obrigatória

* .NET 10
* ASP.NET Core Minimal APIs
* Entity Framework Core
* InMemory Database Provider
* IMemoryCache
* FluentValidation
* JWT Authentication
* Swagger/OpenAPI
* Docker
* Serilog
* Rate Limiting
* Health Checks
* OpenTelemetry

---

# Banco de dados (IMPORTANTE)

Neste momento, utilizar apenas:

```csharp id="wthw8u"
UseInMemoryDatabase()
```

Objetivo:

* simplificar execução local;
* evitar dependência de PostgreSQL inicialmente;
* facilitar testes;
* permitir evolução futura para banco relacional.

A arquitetura deve permitir troca futura para:

* PostgreSQL;
* SQL Server;
* MongoDB;

sem impacto significativo nas regras de negócio.

---

# Cache (IMPORTANTE)

Não utilizar Redis.

Utilizar apenas:

```csharp id="3l9brn"
IMemoryCache
```

Casos de uso do cache:

* produtos financeiros;
* regras de suitability;
* respostas de consultas;
* dados estáticos.

A implementação deve:

* encapsular cache em abstrações;
* possuir expiração;
* evitar código acoplado ao framework;
* permitir troca futura para Redis sem alterar regras de negócio.

---

# Arquitetura obrigatória 🏛️

A solução deve seguir princípios de:

* Clean Architecture
* Vertical Slice Architecture
* SOLID
* Separation of Concerns
* Domain-Driven Design (DDD) simplificado
* CQRS simplificado
* Result Pattern
* Dependency Injection nativa

A arquitetura deve priorizar:

* baixo acoplamento;
* alta coesão;
* facilidade de manutenção;
* escalabilidade;
* testabilidade;
* organização enterprise.

---

# Estrutura da Solution

```text id="bqyh1o"
/src

  /Suitability.Api
    /Endpoints
    /Filters
    /Middleware
    /Configurations
    /Extensions

  /Suitability.Application
    /Features
      /EvaluateSuitability
      /ValidateProduct
    /DTOs
    /Interfaces
    /Services
    /Validators
    /Behaviors
    /Caching

  /Suitability.Domain
    /Entities
    /Enums
    /Rules
    /ValueObjects
    /Events

  /Suitability.Infrastructure
    /Persistence
    /Repositories
    /Auth
    /Logging
    /Caching
    /ExternalServices

/tests

  /Suitability.UnitTests
  /Suitability.IntegrationTests
```

---

# Padrão Vertical Slice

Cada feature deve ficar isolada.

Exemplo:

```text id="c5ghdn"
/Features
  /EvaluateSuitability
    Endpoint.cs
    Request.cs
    Response.cs
    Validator.cs
    Handler.cs
```

Evitar:

* Services gigantes;
* Controllers tradicionais;
* Camadas anêmicas;
* Arquivos genéricos excessivos.

---

# Requisitos funcionais

## 1. Avaliação de suitability

Endpoint:

```http id="ax8vg8"
POST /api/suitability/evaluate
```

Entrada:

```json id="l5zwaf"
{
  "idade": 24,
  "rendaMensal": 10000,
  "objetivo": "crescimento",
  "prazo": "longo",
  "experiencia": "media",
  "toleranciaRisco": 8,
  "possuiReservaEmergencia": true
}
```

Saída:

```json id="jk84wv"
{
  "score": 78,
  "perfil": "MODERADO",
  "validade": "2028-05-10",
  "produtosPermitidos": [
    "CDB",
    "TESOURO_IPCA",
    "FUNDO_MULTIMERCADO"
  ]
}
```

---

## 2. Validação de produto

Endpoint:

```http id="9h2s5e"
POST /api/suitability/validate-product
```

Entrada:

```json id="7l3vrh"
{
  "clienteId": 1,
  "produto": "COE_ALAVANCADO"
}
```

Saída:

```json id="ys4h6j"
{
  "permitido": false,
  "motivo": "Produto incompatível com perfil CONSERVADOR",
  "codigoCompliance": "SUITABILITY_001"
}
```

---

# Regras de negócio

## Score

O cálculo deve considerar:

* experiência;
* renda;
* tolerância a risco;
* prazo;
* reserva de emergência.

## Perfis

| Score  | Perfil      |
| ------ | ----------- |
| 0-39   | Conservador |
| 40-69  | Moderado    |
| 70-100 | Arrojado    |

---

# Entidades obrigatórias

Criar entidades:

* Cliente
* AvaliacaoSuitability
* ProdutoFinanceiro
* ComplianceLog

---

# Persistence Layer

Implementar:

* DbContext
* Seed inicial de produtos financeiros
* Repositórios simples
* Configuração desacoplada da infraestrutura

Mesmo usando InMemory Database, estruturar como se fosse produção.

---

# Minimal APIs

Utilizar recursos modernos:

* Route Groups
* Endpoint Filters
* TypedResults
* IResult
* MapGroup()
* ProblemDetails
* Dependency Injection
* Global Exception Handling

Não utilizar Controllers MVC.

---

# Segurança

Implementar:

* JWT Bearer Authentication
* Authorization Policies
* Claims
* Rate Limiting
* Correlation ID
* Secure Headers

---

# Observabilidade

Adicionar:

* Serilog
* Structured Logging
* OpenTelemetry
* Health Checks
* Tracing
* Request/Response Logging

Criar endpoint:

```http id="wz7ps0"
GET /health
```

---

# Docker

Criar:

* Dockerfile
* docker-compose.yml

Inicialmente apenas com:

* API

Sem PostgreSQL e sem Redis por enquanto.

---

# Qualidade

Adicionar:

* Testes unitários
* Testes de integração
* FluentValidation
* Result Pattern
* Error Handling Pattern

---

# Extras desejáveis

* API Versioning
* Idempotency
* Background Services
* Retry Policies
* Circuit Breaker
* OpenAPI examples

---

# Objetivo final

O projeto deve parecer uma API enterprise moderna de banco digital, demonstrando:

* domínio backend avançado;
* boas práticas modernas do .NET 10;
* arquitetura limpa;
* organização profissional;
* escalabilidade;
* observabilidade;
* segurança;
* separação correta de responsabilidades;
* uso moderno de Minimal APIs recomendado pela Microsoft.
