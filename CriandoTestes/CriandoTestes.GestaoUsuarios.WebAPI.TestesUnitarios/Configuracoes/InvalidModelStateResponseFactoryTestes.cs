using CriandoTestes.GestaoUsuarios.WebAPI.Configuracoes;
using CriandoTestes.GestaoUsuarios.WebAPI.Extensoes;
using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NSubstitute;
using Xunit;

namespace CriandoTestes.GestaoUsuarios.WebAPI.Tests.Configuracoes
{
  public class InvalidModelStateResponseFactoryTestes
  {
    [Fact]
    public void GerarRespostaErro_ReturnsBadRequestObjectResult_WithErroRecurso()
    {
      // Arrange
      var modelState = new ModelStateDictionary();
      modelState.AddModelError("Key", "Mensagem de erro.");
      var actionContext = new ActionContext(
          new DefaultHttpContext(),
          new Microsoft.AspNetCore.Routing.RouteData(),
          new ControllerActionDescriptor(),
          modelState
      );

      var errosEsperados = new List<string> { "Mensagem de erro." };

      // Act
      var resultado = InvalidModelStateResponseFactory.GerarRespostaErro(actionContext);

      // Assert
      var badRequestResult = Assert.IsType<BadRequestObjectResult>(resultado);
      var response = Assert.IsType<ErroRecurso>(badRequestResult.Value);
      Assert.Equal(errosEsperados, response.Mensagens);
    }
  }
}
