using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Cidades
{
    public interface ICidadePersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Cidade>> GetAllCidadesAsync();
        Task<Cidade> GetCidadeByIdAsync(int cidadeId);
    }
}
