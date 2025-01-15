namespace CriandoTestes.GestaoUsuarios.WebAPI.Recursos;

public sealed record ErroRecurso
{
  public bool Sucesso { get; init; }
  public IEnumerable<string> Mensagens { get; init; }

  public ErroRecurso(IEnumerable<string> mensagens)
  {
    Sucesso = false;
    Mensagens = mensagens ?? Enumerable.Empty<string>();
  }
}
