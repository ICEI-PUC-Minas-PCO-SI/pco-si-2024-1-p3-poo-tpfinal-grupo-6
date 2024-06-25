
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.ParametrosEleicoes;

namespace UrnaEletronica.Servico.Servicos.Contratos.ParametrosEleicoes
{
    public interface IParametroEleicaoServico
    {
        Task<IEnumerable<ParametroEleicaoDto>> GetParametrosAsync();
        Task<ParametroEleicaoDto> GetParametroByIdAsync(int parametroId);
        Task<ParametroEleicaoDto> CreateParametroEleicao(ParametroEleicaoDto parametroEleicaoDto);
        Task<ParametroEleicaoDto> UpdateParametroEleicao(int parametroEleicaoId, ParametroEleicaoDto parametroEleicaoDto);
    }
}
