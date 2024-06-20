using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.LogsVotosBatchs
{
    public class LogVotosErrosPersistencia : SharedPersistencia, ILogVotosErrosPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public LogVotosErrosPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<LogVotosBatchErros>> GetAllLogErrosAsync()
        {
            IQueryable<LogVotosBatchErros> query = _contexto.LogsVotosBatchesErros
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToListAsync();
        }
    }
}
