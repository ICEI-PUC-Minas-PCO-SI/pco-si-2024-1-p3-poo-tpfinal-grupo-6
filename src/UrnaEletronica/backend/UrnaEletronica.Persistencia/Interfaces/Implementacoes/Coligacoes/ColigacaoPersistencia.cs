using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Coligacoes
{
    public class ColigacaoPersistencia : SharedPersistencia, IColigacaoPersistencia
    {

            private readonly UrnaEletronicaContexto _contexto;

            public ColigacaoPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
            {
                _contexto = contexto;
            }

            public async Task<IEnumerable<Coligacao>> GetAllCidadesAsync()
            {
                IQueryable<Coligacao> query = _contexto.Coligacoes
                    .AsNoTracking()
                    .OrderBy(c => c.Id);

                return await query.ToListAsync();
            }

            public async Task<Coligacao> GetCidadeByIdAsync(int coligacaoId)
            {
                IQueryable<Coligacao> query = _contexto.Coligacoes
                  .AsNoTracking()
                  .OrderBy(u => u.Id)
                  .Where(u => u.Id == coligacaoId);

                return await query.FirstOrDefaultAsync();
            }
        }
    }
}
