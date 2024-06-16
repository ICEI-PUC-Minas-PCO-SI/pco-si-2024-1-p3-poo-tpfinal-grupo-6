using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Partidos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Partidos
{
    public interface IPartidoPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Partido>> GetAllPartidosAsync();
        Task<Partido> GetPartidoByIdAsync(int partidoId);
    }
}