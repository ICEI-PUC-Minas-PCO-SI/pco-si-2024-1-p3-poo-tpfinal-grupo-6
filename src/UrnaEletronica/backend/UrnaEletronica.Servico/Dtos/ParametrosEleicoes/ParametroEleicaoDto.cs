using UrnaEletronica.Servico.Dtos.Cidades;

namespace UrnaEletronica.Servico.Dtos.ParametrosEleicoes
{
    public class ParametroEleicaoDto
    {
        public int Id { get; set; }
        public bool PrimeiroTurno { get; set; }
        public bool SegundoTurno { get; set; }
        public int QtdVotosSomentePrimeiroTurno { get; set; }
        public int QtdCadeiras { get; set; }
        public DateTime DataEleicaoPrimeiroTurno { get; set; }
        public DateTime DataEleicaoSegundoTurno { get; set; }
        public int CidadeId { get; set; }
        public CidadeDto Cidade { get; set; }
    }
}
