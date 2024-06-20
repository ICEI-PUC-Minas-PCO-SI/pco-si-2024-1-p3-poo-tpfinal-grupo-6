using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Coligacoes
{
    public interface IColigacaoPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Coligacao>> GetAllColigacoesAsync();
        Task<Coligacao> GetColigacaoByIdAsync(int cidadeId);
        Task<bool> CalcularVotosColigacao();

    }
}
