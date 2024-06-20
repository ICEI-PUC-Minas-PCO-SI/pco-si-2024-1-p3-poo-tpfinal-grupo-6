using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Coligacoes;

namespace UrnaEletronica.Dominio.Modelos.Partidos
{
    public class Partido
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int ColigacaoId {  get; set; }
        public Coligacao Coligacao { get; set; }
        public IEnumerable<Candidato> Candidatos { get; set; }
    }
}
