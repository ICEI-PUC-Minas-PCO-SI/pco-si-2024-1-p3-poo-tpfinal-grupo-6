using UrnaEletronica.Servico.Dtos.Resultado;

namespace UrnaEletronica.Servico.Servicos.Contratos.Eleicoes
{
    public interface IEleicaoExecutivaServico
    {
        Task<IEnumerable<ResultadoDto>> CalcularResultado();
        bool EleicaoEmAndamento();
        bool EleicaoEncerrada();
        bool EncerrarEleicao();
        bool IniciarEleicao();
    }
}
