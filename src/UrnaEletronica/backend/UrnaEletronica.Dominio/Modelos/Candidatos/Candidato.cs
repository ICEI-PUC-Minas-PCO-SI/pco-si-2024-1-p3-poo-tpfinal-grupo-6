using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Dominio.Modelos.Partidos;

namespace UrnaEletronica.Dominio.Modelos.Candidatos
{
    public class Candidato
    {
        public int Id { get; set; }
        public bool EhExecutivo { get; set; } = false;
        public bool EhLegislativo { get; set; } = false;
        public string Nome { get; set; }
        public int QtdVotos { get; set; }
        public bool VotosValidos { get; set; } = false;
        public DateTime DataNascimento { get; set; }
        public string TipoCandidatura { get; set; }
        public string FotoURL { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
        public int PartidoId { get; set; }
        public Partido Partido { get; set; }
        public int ColigacoaId { get; set; }
        public Coligacao Coligacao { get; set; }
    }
}
