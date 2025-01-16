using System.Text.RegularExpressions;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using CriandoTestes.GestaoUsuarios.WebAPI.TestesUnitarios.Utilitarios;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesUnitarios.Recursos;

public class CriarUsuarioRecursoTests
{
  [Fact]
  public void CriarUsuarioRecurso_ModelValida_NaoDeveGerarErrosValidacao()
  {
    // Arrange
    var model = new CriarUsuarioRecurso
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
      Senha = "SecurePassword123"
    };

    // Act
    var validationResults = ModelStateValidator.Validate(model);

    // Assert
    Assert.Empty(validationResults);
  }

  [Fact]
  public void CriarUsuarioRecurso_SemNome_DeveGerarErrosValidacao()
  {
    // Arrange
    var model = new CriarUsuarioRecurso
    {
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
      Senha = "SecurePassword123"
    };

    // Act
    var validationResults = ModelStateValidator.Validate(model);

    // Assert
    Assert.Contains(validationResults, v => Regex.IsMatch(v.ErrorMessage!, "nome"));
  }

  [Fact]
  public void CriarUsuarioRecurso_EmailInvalido_DeveGerarErrosValidacao()
  {
    // Arrange
    var model = new CriarUsuarioRecurso
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "invalid-email",
      Senha = "SecurePassword123"
    };

    // Act
    var validationResults = ModelStateValidator.Validate(model);

    // Assert
    Assert.Contains(validationResults, v => Regex.IsMatch(v.ErrorMessage!, "email"));
  }

  [Fact]
  public void CriarUsuarioRecurso_FaltandoSenha_DeveGerarErrosValidacao()
  {
    // Arrange
    var model = new CriarUsuarioRecurso
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com"
    };

    // Act
    var validationResults = ModelStateValidator.Validate(model);

    // Assert
    Assert.Contains(validationResults, v => Regex.IsMatch(v.ErrorMessage!, "senha"));
  }

  [Fact]
  public void CriarUsuarioRecurso_FaltandoDataNascimento_DeveGerarErrosValidacao()
  {
    // Arrange
    var model = new CriarUsuarioRecurso
    {
      Nome = "John Doe",
      Email = "john.doe@example.com",
      Senha = "SecurePassword123"
    };

    // Act
    var validationResults = ModelStateValidator.Validate(model);

    // Assert
    Assert.Contains(validationResults, v => Regex.IsMatch(v.ErrorMessage!, "data de nascimento"));
  }
}
