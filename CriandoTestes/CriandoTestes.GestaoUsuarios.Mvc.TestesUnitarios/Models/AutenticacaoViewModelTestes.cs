using CriandoTestes.GestaoUsuarios.Mvc.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CriandoTestes.GestaoUsuarios.Mvc.TestesUnitarios.Models;

public class AutenticacaoViewModelTestes
{
  [Fact]
  public void Email_Valido_DevePassarValidacao()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "valid.email@example.com",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.True(isValid);
    Assert.Empty(validationResults);
  }

  [Fact]
  public void Email_Nulo_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = null!,
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "O email é obrigatório.");
  }

  [Fact]
  public void Email_Invalido_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "invalid-email",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "O email é inválido.");
  }

  [Fact]
  public void Senha_Valida_DevePassarValidacao()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "valid.email@example.com",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.True(isValid);
    Assert.Empty(validationResults);
  }

  [Fact]
  public void Senha_Nula_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "valid.email@example.com",
      Senha = null!
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "A senha é obrigatória.");
  }

  [Fact]
  public void Senha_Vazia_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new AutenticacaoViewModel
    {
      Email = "valid.email@example.com",
      Senha = ""
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "A senha é obrigatória.");
  }
}
