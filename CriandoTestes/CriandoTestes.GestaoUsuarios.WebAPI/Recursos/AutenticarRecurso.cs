using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Recursos;

// Como estamos usando esse recurso apenas para demonstrar como testar autenticação, não vamos incluir na cobertura
// de testes.
[ExcludeFromCodeCoverage]
public sealed record AutenticarRecurso
{
  [Required(ErrorMessage = "O email é obrigatório.")]
  [EmailAddress(ErrorMessage = "O email informado não é válido.")]
  public string Email { get; init; } = null!;

  [Required(ErrorMessage = "A senha é obrigatória.")]
  public string Senha { get; init; } = null!;
}
