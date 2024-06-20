
using UrnaEletronica.Servico.Dtos.Coligacoes;

namespace UrnaEletronica.Servico.Servicos.Contratos.Coligacoes
{
    public interface IColigacaoServico
    {
        Task<IEnumerable<ColigacaoDto>> GetAllColigacoesAsync();
        Task<ColigacaoDto> GetColigacaoByIdAsync(int coligacaoId);
        Task<ColigacaoDto> CreateColigacao(ColigacaoDto coligacaoDto);
        Task<ColigacaoDto> UpdateColigacao(int coligacaoId, ColigacaoDto coligacaoDto);
        Task<bool> DeleteColigacao(int coligacaoId);
        Task<bool> CalcularVotosColigacao();
    }
}
