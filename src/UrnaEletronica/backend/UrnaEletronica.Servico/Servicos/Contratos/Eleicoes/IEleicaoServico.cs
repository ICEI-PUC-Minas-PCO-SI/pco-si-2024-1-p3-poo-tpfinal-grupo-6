using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Servico.Dtos.Eleicoes;
using UrnaEletronica.Servico.Dtos.Resultado;

namespace UrnaEletronica.Servico.Servicos.Contratos.Eleicoes
{
    public interface IEleicaoServico
    {
        Task<IEnumerable<EleicaoDto>> GetAllEleicoesAsync(); 
        Task<EleicaoDto> GetEleicaoByIdAsync(int eleicaoId);
        Task<IEnumerable<ResultadoDto>> CalcularResultado(int eleicaoId);
        Task<bool> EleicaoEmAndamento(int eleicaoId);
        Task<bool> EleicaoEncerrada(int eleicaoId);
        Task<bool> EncerrarEleicaoAsync(int eleicaoId);
        Task<bool> IniciarEleicaoAsync();
    }
}
