using System.ComponentModel.DataAnnotations;

namespace CriandoTestes.GestaoUsuarios.Mvc.Models;

public class UsuarioViewModel
{
  [Required(ErrorMessage = "O nome é obrigatório.")]
  public string Nome { get; set; } = null!;

  [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
  [DataType(DataType.Date)]
  public DateTime? DataNascimento { get; set; }

  [Required(ErrorMessage = "O email é obrigatório.")]
  [EmailAddress(ErrorMessage = "É necessário informar um email válido.")]
  public string Email { get; set; } = null!;

  [Required(ErrorMessage = "A senha é obrigatória.")]
  [DataType(DataType.Password)]
  public string Senha { get; set; } = null!;
}
