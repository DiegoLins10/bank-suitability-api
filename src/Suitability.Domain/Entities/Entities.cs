namespace Suitability.Domain.Entities;
public class Cliente { public int Id { get; set; } public string Nome { get; set; } = string.Empty; public string PerfilAtual { get; set; } = "CONSERVADOR"; }
public class ProdutoFinanceiro { public int Id { get; set; } public string Nome { get; set; } = string.Empty; public int RiscoMinimo { get; set; } }
public class AvaliacaoSuitability { public int Id { get; set; } public int Score { get; set; } public string Perfil { get; set; } = string.Empty; public DateOnly Validade { get; set; } }
public class ComplianceLog { public int Id { get; set; } public int ClienteId { get; set; } public string Codigo { get; set; } = string.Empty; public string Mensagem { get; set; } = string.Empty; public DateTime CriadoEm { get; set; } = DateTime.UtcNow; }
