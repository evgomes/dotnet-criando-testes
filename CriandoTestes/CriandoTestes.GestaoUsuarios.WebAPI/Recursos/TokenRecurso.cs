using System.Diagnostics.CodeAnalysis;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Recursos;

[ExcludeFromCodeCoverage]
public sealed class TokenRecurso
{
  public required string Token { get; init; } = null!;
}
