using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Servico.Dtos.Cidades;

namespace UrnaEletronica.Servico.Servicos.Contratos.Cidades
{
    public interface ICidadeServico
    {
        Task<IEnumerable<CidadeDto>> GetAllCidadesAsync();
        Task<CidadeDto> GetCidadeByIdAsync(int cidadeId);
        Task<CidadeDto> CreateCidade(CidadeDto cidadeDto);
        Task<CidadeDto> UpdateCidade(int cidadeId, CidadeDto cidadeDto);
        Task<bool> DeleteCidade(int cidadeId);
    }
}
