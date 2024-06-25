using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Resultados;

namespace UrnaEletronica.Dominio.Modelos.Eleicoes
{
    public abstract class Eleicao
    {
        public int Id { get; set; }
        public string TipoEleicao { get; set; }
        public bool IniciarVotacao { get; set; } = false;
        public bool EncerrarVotacao { get; set; } = false;
        public DateTime DataHoraInicioVotacao { get; set; }
        public DateTime DataHoraFimVotacao { get; set; }

        public abstract List<Resultado> CalcularResultado(ParametroEleicao parametroEleicao, IEnumerable<Candidato> Candidatos);
        public bool IniciarEleicao()
        {
            try
            {
                if (!IniciarVotacao && !EncerrarVotacao)
                {
                    DataHoraInicioVotacao = DateTime.Now;
                    IniciarVotacao = true;
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao inciar votacao. Erro: ${ex.Message}", ex);
            }
        }
        public bool EncerrarEleicao() 
        {
            try
            {
                if (IniciarVotacao && !EncerrarVotacao)
                {
                    DataHoraFimVotacao = DateTime.Now;
                    EncerrarVotacao = true;
                    return true;
                }
                return false;
            }
            catch (System.Exception ex)
            {

                throw new Exception($"Falha ao encerrar votacao. Erro: ${ex.Message}", ex);
            }
        }
        public bool EleicaoEmAdamento() 
        {  
            return IniciarVotacao; 
        }
        public bool EleicaoEncerrada()
        {
            return EncerrarVotacao;
        }

    }
}
