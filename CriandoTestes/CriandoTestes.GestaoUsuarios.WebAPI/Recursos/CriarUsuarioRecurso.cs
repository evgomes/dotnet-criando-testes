using System.ComponentModel.DataAnnotations;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Recursos;

public sealed record CriarUsuarioRecurso
{
  [Required(ErrorMessage = "O nome é obrigatório.")]
  public string Nome { get; init; } = null!;

  [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
  public DateTime DataNascimento { get; init; }

  [Required(ErrorMessage = "O email é obrigatório.")]
  [EmailAddress(ErrorMessage = "É necessário informar um email válido.")]
  public string Email { get; init; } = null!;

  [Required(ErrorMessage = "A senha é obrigatória.")]
  public string Senha { get; init; } = null!;
}
