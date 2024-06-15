using Microsoft.AspNetCore.Mvc;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Cidades;

namespace UrnaEletronica.api.Controllers.Cidades
{
    public class CidadesController : Controller 
    {
        public CidadesController(ICidadeServico cidadeServico, )
        {
            
        }

        Task<IEnumerable<CidadeDto>> GetAllCidadesAsync();
        Task<CidadeDto> GetCidadeByIdAsync(int cidadeId);
        Task<CidadeDto> CreateCidade(CidadeDto cidadeDto);
        Task<CidadeDto> UpdateCidade(int cidadeId, CidadeDto cidadeDto);
        Task<bool> DeleteCidade(int cidadeId);
    }
}
