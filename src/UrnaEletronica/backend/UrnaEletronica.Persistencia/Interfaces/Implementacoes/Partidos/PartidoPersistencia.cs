using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Partidos;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Partidos;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Partidos         
{
    public class PartidoPersistencia : SharedPersistencia, IPartidoPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public PartidoPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Partido>> GetAllPartidosAsync()
        {
            IQueryable<Partido> query = _contexto.Partidos
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToListAsync();
        }

        public async Task<Partido> GetPartidoByIdAsync(int partidoId)
        {
            IQueryable<Partido> query = _contexto.Partidos
              .AsNoTracking()
              .OrderBy(u => u.Id)
              .Where(u => u.Id == partidoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
