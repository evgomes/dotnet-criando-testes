using CriandoTestes.GestaoUsuarios.Dominio.Models;

namespace CriandoTestes.GestaoUsuarios.Dominio.Repositorios;

public interface IUsuarioRepositorio
{
  Task<Usuario?> BuscarPorEmailAsync(string email, CancellationToken cancellationToken);
  Task CriarAsync(Usuario usuario, CancellationToken cancellationToken);
}
