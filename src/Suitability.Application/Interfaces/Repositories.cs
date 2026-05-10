namespace Suitability.Application.Interfaces;
public interface IProdutoRepository { Task<IReadOnlyList<string>> ObterProdutosPermitidosAsync(int score, CancellationToken ct); }
public interface IComplianceRepository { Task RegistrarAsync(int clienteId, string codigo, string mensagem, CancellationToken ct); }
