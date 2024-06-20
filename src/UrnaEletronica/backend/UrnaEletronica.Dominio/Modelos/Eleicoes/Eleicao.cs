using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Resultados;

namespace UrnaEletronica.Dominio.Modelos.Eleicoes
{
    public abstract class Eleicao
    {
        public bool IniciarVotacao { get; set; } = false;
        public bool EncerrarVotacao { get; set; } = false;
        public DateTime DataHoraInicioVotacao { get; set; }
        public DateTime DataHoraFimVotacao { get; set; }

        public abstract IEnumerable<Resultado> CalcularResultado(ParametroEleicao parametroEleicao, IEnumerable<Candidato> Candidatos);
        public abstract bool IniciarEleicao();
        public abstract bool EncerrarEleicao();
        public abstract bool EleicaoEmAdamento();
        public abstract bool EleicaoEncerrada();

    }
}
