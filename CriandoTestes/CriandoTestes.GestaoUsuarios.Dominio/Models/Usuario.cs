using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CriandoTestes.GestaoUsuarios.Dominio.Models;

public class Usuario
{
  public int Id { get; set; }
  public string Nome { get; private set; } = null!;
  public DateTime DataNascimento { get; set; }
  public string Email { get; set; } = null!;
  public string Senha { get; set; } = null!;

  [ExcludeFromCodeCoverage]
  // Construtor vazior para EF Core.
  protected Usuario()
  {
  }

  public Usuario(string nome, DateTime dataNascimento, string email, string senha)
  {
    ValidarNome(nome);
    ValidarDataNascimento(dataNascimento);
    ValidarEmail(email);
    ValidarSenha(senha);

    Nome = nome;
    DataNascimento = dataNascimento;
    Email = email;
    Senha = senha;
  }

  private static void ValidarNome(string nome)
  {
    if (string.IsNullOrWhiteSpace(nome))
    {
      throw new ArgumentNullException("O nome é obrigatório.");
    }
  }

  private static void ValidarDataNascimento(DateTime dataNascimento)
  {
    if (dataNascimento.Date > DateTime.Now.Date)
    {
      throw new ArgumentOutOfRangeException("A data de nascimento não pode ser superior a data atual.");
    }
  }

  private static void ValidarEmail(string email)
  {
    if (string.IsNullOrWhiteSpace(email))
    {
      throw new ArgumentNullException("O email é obrigatório.");
    }

    if (!new EmailAddressAttribute().IsValid(email))
    {
      throw new ArgumentException("É necessário informar um email válido.");
    }
  }

  private static void ValidarSenha(string senha)
  {
    if (string.IsNullOrWhiteSpace(senha))
    {
      throw new ArgumentNullException("A senha é obrigatória.");
    }
  }
}
