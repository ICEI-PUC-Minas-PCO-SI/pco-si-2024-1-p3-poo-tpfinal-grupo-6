using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Usuarios;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Usuarios
{
    public class UsuarioPersistencia : SharedPersistencia, IUsuarioPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public UsuarioPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosAsync()
        {
            IQueryable<Usuario> query = _contexto.Users
                .AsNoTracking()
                .OrderBy(u => u.Id);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuariosByNomeAsync(string nome)
        {
            IQueryable<Usuario> query = _contexto.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Where(u => u.Nome.ToLower().Contains(nome.ToLower()));
            
            return await query.ToListAsync();
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int usuarioId)
        {
            IQueryable<Usuario> query = _contexto.Users
               .AsNoTracking()
               .OrderBy(u => u.Id)
               .Where(u => u.Id == usuarioId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Usuario> GetUsuarioByUserNameAsync(string userName)
        {
            IQueryable<Usuario> query = _contexto.Users
              .AsNoTracking()
              .OrderBy(u => u.Id)
              .Where(u => u.UserName == userName);

            return await query.FirstOrDefaultAsync();
        }
    }
}
