using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Usuarios
{
    public interface IUsuarioPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosByNomeAsync(string nome);
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task<Usuario> GetUsuarioByIdAsync(int usuarioId);
        Task<Usuario> GetUsuarioByUserNameAsync(string userName);
    }
}
