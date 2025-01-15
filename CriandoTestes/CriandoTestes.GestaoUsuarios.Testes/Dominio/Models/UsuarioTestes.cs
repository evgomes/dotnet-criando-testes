using CriandoTestes.GestaoUsuarios.Dominio.Models;

namespace CriandoTestes.GestaoUsuarios.Tests;

public class UsuarioTestes
{
  [Fact]
  public void Construtor_ParametrosValidos_DeveCriarUsuario()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = new DateTime(1990, 1, 1);
    var email = "john.doe@example.com";
    var senha = "password123";

    // Act
    var usuario = new Usuario(nome, dataNascimento, email, senha);

    // Assert
    Assert.Equal(nome, usuario.Nome);
    Assert.Equal(dataNascimento, usuario.DataNascimento);
    Assert.Equal(email, usuario.Email);
    Assert.Equal(senha, usuario.Senha);
  }

  [Fact]
  public void Construtor_NomeNulo_DeveLancarArgumentNullException()
  {
    // Arrange
    var dataNascimento = new DateTime(1990, 1, 1);
    var email = "john.doe@example.com";
    var senha = "password123";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentNullException>(() => new Usuario(null!, dataNascimento, email, senha));
    Assert.Matches("nome", excecao.Message);
  }


  [Fact]
  public void Construtor_NomeVazio_DeveLancarArgumentNullException()
  {
    // Arrange
    var dataNascimento = new DateTime(1990, 1, 1);
    var email = "john.doe@example.com";
    var senha = "password123";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentNullException>(() => new Usuario("", dataNascimento, email, senha));
    Assert.Matches("nome", excecao.Message);
  }

  [Fact]
  public void Construtor_DataNascimentoFutura_DeveLancarArgumentOutOfRangeException()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = DateTime.Now.AddDays(1);
    var email = "john.doe@example.com";
    var senha = "password123";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentOutOfRangeException>(() => new Usuario(nome, dataNascimento, email, senha));
    Assert.Matches("data de nascimento", excecao.Message);
  }

  [Fact]
  public void Construtor_EmailNulo_DeveLancarArgumentNullException()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = new DateTime(1990, 1, 1);
    var senha = "password123";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentNullException>(() => new Usuario(nome, dataNascimento, null!, senha));
    Assert.Matches("email", excecao.Message);
  }

  [Fact]
  public void Construtor_EmailVazio_DeveLancarArgumentNullException()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = new DateTime(1990, 1, 1);
    var senha = "password123";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentNullException>(() => new Usuario(nome, dataNascimento, "", senha));
    Assert.Matches("email", excecao.Message);
  }

  [Fact]
  public void Construtor_EmailInvalido_DeveLancarArgumentException()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = new DateTime(1990, 1, 1);
    var senha = "password123";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentException>(() => new Usuario(nome, dataNascimento, "invalido", senha));
    Assert.Matches("válido", excecao.Message);
  }

  [Fact]
  public void Construtor_SenhaNula_DeveLancarArgumentNullException()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = new DateTime(1990, 1, 1);
    var email = "john.doe@example.com";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentNullException>(() => new Usuario(nome, dataNascimento, email, null!));
    Assert.Matches("senha", excecao.Message);
  }

  [Fact]
  public void Construtor_SenhaVazia_DeveLancarArgumentNullException()
  {
    // Arrange
    var nome = "John Doe";
    var dataNascimento = new DateTime(1990, 1, 1);
    var email = "john.doe@example.com";

    // Act & Assert
    var excecao = Assert.Throws<ArgumentNullException>(() => new Usuario(nome, dataNascimento, email, ""));
    Assert.Matches("senha", excecao.Message);
  }
}
