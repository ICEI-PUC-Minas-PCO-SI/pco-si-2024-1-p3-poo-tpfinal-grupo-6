using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Cidades
{
    public class CidadePersistencia : SharedPersistencia, ICidadePersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public CidadePersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Cidade>> GetAllCidadesAsync()
        {
            IQueryable<Cidade> query = _contexto.Cidades
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToListAsync();
        }

        public async Task<Cidade> GetCidadeByIdAsync(int cidadeId)
        {
            IQueryable<Cidade> query = _contexto.Cidades
              .AsNoTracking()
              .OrderBy(u => u.Id)
              .Where(u => u.Id == cidadeId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
