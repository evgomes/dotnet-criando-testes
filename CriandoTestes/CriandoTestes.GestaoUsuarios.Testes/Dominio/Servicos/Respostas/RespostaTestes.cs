using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Respostas;

namespace CriandoTestes.GestaoUsuarios.Dominio.Tests.Servicos.Respostas;

public class RespostaTestes
{
  [Fact]
  public void ConcluidaComSucesso_DeveCriarResposaConcluidaComSucesso()
  {
    // Arrange
    var dados = "Dados de Teste";

    // Act
    var result = Resposta<string>.ConcluidaComSucesso(dados);

    // Assert
    Assert.True(result.Sucesso);
    Assert.Null(result.Mensagem);
    Assert.Equal(dados, result.Dados);
  }

  [Fact]
  public void Erro_DeveCriarRespostaErro()
  {
    // Arrange
    var mensagem = "Mensagem de erro";

    // Act
    var result = Resposta<string>.Erro(mensagem);

    // Assert
    Assert.False(result.Sucesso);
    Assert.Equal(mensagem, result.Mensagem);
    Assert.Null(result.Dados);
  }
}
