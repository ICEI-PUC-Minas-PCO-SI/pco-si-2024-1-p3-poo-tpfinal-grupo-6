using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Dominio.Modelos.Eleicoes;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Eleicoes;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Eleicoes
{
    public class EleicaoPersistencia : SharedPersistencia, IEleicaoPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public EleicaoPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Eleicao>> GetAllEleicoesAsync()
        {
            IQueryable<Eleicao> query = _contexto.Eleicoes
                .AsNoTracking();

            return await query.ToListAsync();
        }

        public async Task<Eleicao> GetEleicaoByIdAsync(int eleicaoId)
        {
            IQueryable<Eleicao> query = _contexto.Eleicoes
                .AsNoTracking()
                .Where(u => u.Id == eleicaoId);

            return await query.FirstOrDefaultAsync();
        }

        public Eleicao GetEleicaoByIdUpdateAsync(int eleicaoId)
        {
            return _contexto.Eleicoes.Find(eleicaoId);
        }
    }
}
