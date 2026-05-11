# Suitability API 🚀🏦

Uma API de **Suitability bancária** feita com **.NET 10** utilizando Clean Architecture e Vertical Slice Architecture, para simular o processo utilizado por bancos e corretoras na avaliação do perfil de investidor antes da recomendação de produtos financeiros. 📊

> By Diego Lins ✨

---

## 🌟 Ideia da API

A proposta é simples e poderosa:

- entender o perfil de risco do cliente 👤
- calcular um score de suitability 📈
- classificar o perfil como:
  - `CONSERVADOR`
  - `MODERADO`
  - `ARROJADO`
- validar se um produto financeiro pode ser ofertado ✅❌
- registrar compliance/auditoria 🧾

Em outras palavras: uma base moderna de API para cenário real de banco digital. 💳

---

## 🧠 Regras de Negócio

### 1) Avaliação de Suitability
Endpoint: `POST /api/suitability/evaluate`

A API calcula o score considerando:

- experiência do investidor 🎓
- renda mensal 💰
- tolerância a risco ⚠️
- prazo do investimento ⏳
- reserva de emergência 🛟

### 2) Classificação de Perfil

- `0-39` → `CONSERVADOR`
- `40-69` → `MODERADO`
- `70-100` → `ARROJADO`

### 3) Validação de Produto
Endpoint: `POST /api/suitability/validate-product`

- informa se o produto pode ser ofertado
- retorna motivo da decisão
- retorna código de compliance (`SUITABILITY_XXX`) 🛡️

---

## 🧱 Estrutura da Solução

```text
/src
  /Suitability.Api
  /Suitability.Application
  /Suitability.Domain
  /Suitability.Infrastructure
/tests
  /Suitability.UnitTests
  /Suitability.IntegrationTests
```

### 📦 Camadas

- **Suitability.Api**
  - Minimal APIs
  - configuração de autenticação/autorização
  - rate limiting, health check, swagger

- **Suitability.Application**
  - features em Vertical Slice
  - validações com FluentValidation
  - handlers com regras de aplicação

- **Suitability.Domain**
  - entidades e enums centrais do domínio
  - linguagem de negócio da API

- **Suitability.Infrastructure**
  - EF Core com `UseInMemoryDatabase()`
  - repositórios
  - persistência desacoplada da regra de negócio

- **Tests**
  - testes unitários 🧪
  - testes de integração 🔗

---

## 🛠️ Tecnologias Usadas

- **.NET 10**
- **ASP.NET Core Minimal APIs**
- **Entity Framework Core (InMemory Provider)**
- **FluentValidation**
- **JWT Bearer Authentication**
- **Swagger / OpenAPI**
- **Serilog**
- **OpenTelemetry**
- **Rate Limiting**
- **Health Checks**
- **xUnit**

---

## 🔌 Endpoints Principais

### `POST /api/suitability/evaluate`

Resumo: avalia o perfil de investimento do cliente, calcula score de risco e retorna produtos aderentes ao perfil.

#### Request

```json
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

#### Response (200)

```json
{
  "score": 84,
  "perfil": "ARROJADO",
  "validade": "2028-05-10",
  "produtosPermitidos": [
    "CDB",
    "TESOURO_IPCA",
    "FUNDO_MULTIMERCADO",
    "COE_ALAVANCADO"
  ]
}
```

### `POST /api/suitability/validate-product`

Resumo: valida se um produto financeiro pode ser ofertado ao cliente e retorna decisão de compliance.

#### Request

```json
{
  "clienteId": 1,
  "produto": "COE_ALAVANCADO"
}
```

#### Response (200)

```json
{
  "permitido": false,
  "motivo": "Produto incompatível com perfil CONSERVADOR",
  "codigoCompliance": "SUITABILITY_001"
}
```

### `GET /health`

Resumo: verifica a saúde da aplicação para monitoramento e disponibilidade do serviço.

#### Response (200)

```text
Healthy
```

---

## ▶️ Como rodar localmente

```bash
dotnet restore
dotnet build
dotnet run --project src/Suitability.Api
```

Swagger (desenvolvimento):

- `http://localhost:<porta>/swagger`

---

## ✅ Qualidade

- arquitetura organizada para evolução futura
- separação de responsabilidades
- foco em testabilidade e observabilidade
- base pronta para evoluir para banco relacional e cache distribuído

---

Feito com foco em engenharia limpa, visão de produto e arquitetura moderna. 🚀

**By Diego Lins** 😎

