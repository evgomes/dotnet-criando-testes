using CriandoTestes.GestaoUsuarios.Mvc.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace CriandoTestes.GestaoUsuarios.Mvc.TestesUnitarios.Models;

public class UsuarioViewModelTestes
{
  [Fact]
  public void Nome_Valido_DevePassarValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
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
  public void Nome_Nulo_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = null!,
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "O nome é obrigatório.");
  }

  [Fact]
  public void Nome_Vazio_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "O nome é obrigatório.");
  }

  [Fact]
  public void DataNascimento_Valida_DevePassarValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
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
  public void DataNascimento_Nula_DeveFalharValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = default,
      Email = "john.doe@example.com",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "A data de nascimento é obrigatória.");
  }

  [Fact]
  public void Email_Valido_DevePassarValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
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
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
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
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "invalid-email",
      Senha = "password123"
    };

    // Act
    var validationResults = new List<ValidationResult>();
    var isValid = Validator.TryValidateObject(viewModel, new ValidationContext(viewModel), validationResults, true);

    // Assert
    Assert.False(isValid);
    Assert.Contains(validationResults, v => v.ErrorMessage == "É necessário informar um email válido.");
  }

  [Fact]
  public void Senha_Valida_DevePassarValidacao()
  {
    // Arrange
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
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
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
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
    var viewModel = new UsuarioViewModel
    {
      Nome = "John Doe",
      DataNascimento = new DateTime(1990, 1, 1),
      Email = "john.doe@example.com",
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
