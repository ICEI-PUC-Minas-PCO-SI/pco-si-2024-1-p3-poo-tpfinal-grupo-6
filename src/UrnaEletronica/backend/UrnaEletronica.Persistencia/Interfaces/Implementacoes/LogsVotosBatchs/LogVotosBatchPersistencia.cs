using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.LogsVotosBatchs

{
    public class LogVotosBatchPersistencia : SharedPersistencia, ILogVotosBatchPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public LogVotosBatchPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<LogVotosBatch>> GetAllLogVotosAsync()
        {
            IQueryable<LogVotosBatch> query = _contexto.LogsVotosBatches
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToListAsync();
        }
    }
}
