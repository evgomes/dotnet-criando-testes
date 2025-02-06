using CriandoTestes.GestaoUsuarios.WebAPI.Recursos;

namespace CriandoTestes.GestaoUsuarios.WebAPI.TestesUnitarios.Recursos;

public class ErroRecursoTestes
{
  [Fact]
  public void Construtor_DeveCriarRecurso()
  {
    // Arrange
    var mensagens = new List<string> { "Erro 1", "Erro 2" };

    // Act
    var recurso = new ErroRecurso(mensagens);

    // Assert
    Assert.False(recurso.Sucesso);
    Assert.Equal(mensagens, recurso.Mensagens);
  }

  [Fact]
  public void Construtor_DeveCriarRecurso_ComEnumeracaoVazia()
  {
    // Act
    var recurso = new ErroRecurso(null!);

    // Assert
    Assert.False(recurso.Sucesso);
    Assert.Equal(Enumerable.Empty<string>(), recurso.Mensagens);
  }
}
