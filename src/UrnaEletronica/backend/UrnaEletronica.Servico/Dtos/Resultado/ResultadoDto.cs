
using UrnaEletronica.Servico.Dtos.Candidatos;

namespace UrnaEletronica.Servico.Dtos.Resultado
{
    public class ResultadoDto
    {
        public int Id { get; set; }
        public int CandidatoId { get; set; }
        public CandidatoDto Candidato { get; set; }
        public int QtdVotos { get; set; }
        public double PercentualVotos { get; set; }
        public bool CandidatoEleito { get; set; }
    }
}
