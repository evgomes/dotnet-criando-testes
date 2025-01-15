namespace CriandoTestes.GestaoUsuarios.Dominio.Servicos.Respostas;

public sealed record Resposta<T>
{
  public bool Sucesso { get; private init; }
  public string? Mensagem { get; private init; }
  public T? Dados { get; private init; }

  private Resposta(bool sucesso, string? mensagem, T? dados)
  {
    Sucesso = sucesso;
    Mensagem = mensagem;
    Dados = dados;
  }

  public static Resposta<T> ConcluidaComSucesso(T dados) => new Resposta<T>(true, null, dados);
  public static Resposta<T> Erro(string mensagem) => new Resposta<T>(false, mensagem, default);
}
