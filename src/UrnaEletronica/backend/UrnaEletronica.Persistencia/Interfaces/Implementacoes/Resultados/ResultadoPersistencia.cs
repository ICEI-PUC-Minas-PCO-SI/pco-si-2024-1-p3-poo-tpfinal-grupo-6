using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.Resultados;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Resultados
{
    public class ResultadoPersistencia : SharedPersistencia, IResultadoPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public ResultadoPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Resultado>> GetAllResultadosAsync()
        {
            IQueryable<Resultado> query = _contexto.Resultados
            .Include(r => r.Candidato)
            .AsNoTracking()
            .OrderBy(r => r.Id);

            return await query.ToListAsync();
        }

        public async Task<Resultado> GetResultadoByCandidatoIdAsync(int candidatoId)
        {
            IQueryable<Resultado> query = _contexto.Resultados
                .Include(r => r.Candidato)
                .AsNoTracking()
                .OrderBy(r => r.Id)
                .Where(r => r.Id == candidatoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Resultado> GetResultadoByIdAsync(int resultadoId)
        {
            IQueryable<Resultado> query = _contexto.Resultados
                .Include(p => p.Candidato)
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Where(u => u.Id == resultadoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
