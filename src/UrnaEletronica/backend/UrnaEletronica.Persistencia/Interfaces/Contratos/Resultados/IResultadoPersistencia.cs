using UrnaEletronica.Dominio.Modelos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Resultados
{
    public interface IResultadoPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Resultado>> GetAllResultadosAsync();
        Task<Resultado> GetResultadoByIdAsync(int resultadoId);
        Task<Resultado> GetResultadoByCandidatoIdAsync(int candidatoId);
    }
}
