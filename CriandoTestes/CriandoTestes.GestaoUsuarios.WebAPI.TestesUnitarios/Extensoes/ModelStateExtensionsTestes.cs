using Microsoft.AspNetCore.Mvc.ModelBinding;
using CriandoTestes.GestaoUsuarios.WebAPI.Extensoes;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Tests.Extensoes;

public class ModelStateExtensionsTestess
{
  [Fact]
  public void GetMensagensErro_QuandoHaErros_DeveRetornarMensagensErro()
  {
    // Arrange
    var modelState = new ModelStateDictionary();
    modelState.AddModelError("chave1", "Erro 1");
    modelState.AddModelError("chave2", "Erro 2");
    modelState.AddModelError("chave3", "Erro 3");

    // Act
    var resultado = modelState.GetMensagensErro();

    // Assert
    Assert.Equal(3, resultado.Count());
    Assert.Contains("Erro 1", resultado);
    Assert.Contains("Erro 2", resultado);
    Assert.Contains("Erro 3", resultado);
  }

  [Fact]
  public void GetMensagensErro_QuandoNaoHaErros_RetornaEnumeracaoVazia()
  {
    // Arrange
    var modelState = new ModelStateDictionary();

    // Act
    var resultado = modelState.GetMensagensErro();

    // Assert
    Assert.Empty(resultado);
  }
}
