using CriandoTestes.GestaoUsuarios.Dados.EFCore.Contextos;
using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Repositorios;
using Microsoft.EntityFrameworkCore;

namespace CriandoTestes.GestaoUsuarios.Dados.EFCore.Repositorios;

public sealed class UsuarioRepositorio : IUsuarioRepositorio
{
  private readonly GestaoUsuariosContexto _contexto;

  public UsuarioRepositorio(GestaoUsuariosContexto contexto)
  {
    _contexto = contexto;
  }

  public async Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken)
    => await _contexto.Usuarios.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

  public async Task CriarAsync(Usuario usuario, CancellationToken cancellationToken)
  {
    await _contexto.AddAsync(usuario, cancellationToken);
    await _contexto.SaveChangesAsync(cancellationToken);
  }
}
