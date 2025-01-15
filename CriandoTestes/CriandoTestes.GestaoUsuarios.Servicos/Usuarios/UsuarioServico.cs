using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Repositorios;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Respostas;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;
using Microsoft.Extensions.Logging;

namespace CriandoTestes.GestaoUsuarios.Servicos.Usuarios;

public class UsuarioServico : IUsuarioServico
{
  private readonly IUsuarioRepositorio _usuarioRepositorio;
  private readonly ILogger<UsuarioServico> _logger;

  public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuarioServico> logger)
  {
    _usuarioRepositorio = usuarioRepositorio;
    _logger = logger;
  }

  public async Task<Resposta<Usuario>> CriarAsync(string nome, DateTime datasNacimento, string email, string senha, CancellationToken cancellationToken)
  {
    var usuarioExistente = await _usuarioRepositorio.BuscarPorEmailAsync(email, cancellationToken);
    if(usuarioExistente != null)
    {
      return Resposta<Usuario>.Erro("O email informado já está sendo utilizado.");
    }

    try
    {
      var usuario = new Usuario(nome, datasNacimento, email, senha);
      await _usuarioRepositorio.CriarAsync(usuario, cancellationToken);
      return Resposta<Usuario>.ConcluidaComSucesso(usuario);
    }
    catch(Exception ex)
    {
      _logger.LogError(ex, "Falha ao criar o usuário.");
      return Resposta<Usuario>.Erro("Falha ao criar o usuário.");
    }
  }
}
