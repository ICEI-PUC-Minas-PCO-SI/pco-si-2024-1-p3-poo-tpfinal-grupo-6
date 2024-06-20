using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Partidos;

namespace UrnaEletronica.Dominio.Modelos.Coligacoes
{
    public class Coligacao
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int QtdVotos { get; set; }
        public IEnumerable<Partido> Partidos { get; set; }
        public IEnumerable<Candidato> Candidatos { get; set; }
    }
}
