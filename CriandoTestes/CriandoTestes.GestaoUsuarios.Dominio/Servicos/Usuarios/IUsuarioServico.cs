using CriandoTestes.GestaoUsuarios.Dominio.Models;
using CriandoTestes.GestaoUsuarios.Dominio.Servicos.Respostas;

namespace CriandoTestes.GestaoUsuarios.Dominio.Servicos.Usuarios;

public interface IUsuarioServico
{
  Task<Resposta<Usuario>> CriarAsync(string nome, DateTime datasNacimento, string email, string senha, CancellationToken cancellationToken);
}
