using System.ComponentModel.DataAnnotations;

namespace CriandoTestes.GestaoUsuarios.Mvc.Models;

public sealed record AutenticacaoViewModel
{
  [Required(ErrorMessage = "O email é obrigatório.")]
  [EmailAddress(ErrorMessage = "O email é inválido.")]
  public string Email { get; set; } = null!;

  [Required(ErrorMessage = "A senha é obrigatória.")]
  [DataType(DataType.Password)]
  public string Senha { get; set; } = null!;
}
