using UrnaEletronica.Dominio.Modelos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.LogsVotosBatchs
{
    public interface ILogVotosBatchPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<LogVotosBatch>> GetAllLogVotosAsync();
    }
}
