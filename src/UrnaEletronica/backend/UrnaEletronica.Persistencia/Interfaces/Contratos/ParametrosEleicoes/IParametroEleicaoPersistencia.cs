
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.ParametrosEleicoes
{
    public interface IParametroEleicaoPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<ParametroEleicao>> GetParametrosAsync();
        Task<ParametroEleicao> GetParametroByIdAsync(int parametroId);
    }
}
