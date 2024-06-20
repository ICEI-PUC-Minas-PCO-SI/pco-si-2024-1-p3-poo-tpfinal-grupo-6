using UrnaEletronica.Servico.Dtos.Resultado;

namespace UrnaEletronica.Servico.Servicos.Contratos.Eleicoes
{
    public interface IEleicaoLegislativaServico
    {
        Task<IEnumerable<ResultadoDto>> CalcularResultado();
        bool EleicaoEmAndamento();
        bool EleicaoEncerrada();
    }
}
