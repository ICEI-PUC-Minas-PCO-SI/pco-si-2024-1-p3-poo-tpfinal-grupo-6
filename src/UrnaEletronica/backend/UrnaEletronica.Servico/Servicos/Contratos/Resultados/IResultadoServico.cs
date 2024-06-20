using UrnaEletronica.Servico.Dtos.Resultado;

namespace UrnaEletronica.Servico.Servicos.Contratos.Resultados
{
    public interface IResultadoServico
    {
        Task<IEnumerable<ResultadoDto>> GetAllResultadosAsync();
        Task<ResultadoDto> GetResultadoByIdAsync(int resultadoId);
        Task<ResultadoDto> CreateResultado(ResultadoDto resultadoDto);
        Task<ResultadoDto> UpdateResultado(int resultadoId, ResultadoDto resultadoDto);
        Task<bool> DeleteResultado(int resultadoId);
    }
}
