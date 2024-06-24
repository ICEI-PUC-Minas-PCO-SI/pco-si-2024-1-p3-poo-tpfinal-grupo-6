using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Eleicoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Eleicoes
{
    public interface IEleicaoPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Eleicao>> GetAllEleicoesAsync();
        Task<Eleicao> GetEleicaoByIdAsync(int eleicaoId);
        Eleicao GetEleicaoByIdUpdateAsync(int eleicaoId);
    }
}
