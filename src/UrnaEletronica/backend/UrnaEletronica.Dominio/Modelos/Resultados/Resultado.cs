using UrnaEletronica.Dominio.Modelos.Candidatos;

namespace UrnaEletronica.Dominio.Modelos.Resultados
{
    public class Resultado
    {
        public int Id { get; set; }
        public int CandidatoId { get; set; }
        public Candidato Candidato { get; set; }
        public int QtdVotos { get; set; }
        public double  PercentualVotos { get; set; }
        public  bool CandidatoEleito { get; set; }
    }
}
