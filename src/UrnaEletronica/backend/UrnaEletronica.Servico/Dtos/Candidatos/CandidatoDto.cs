
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Dtos.Coligacoes;
using UrnaEletronica.Servico.Dtos.Partidos;

namespace UrnaEletronica.Servico.Dtos.Candidatos
{
    public class CandidatoDto
    {
        public int Id { get; set; }
        public bool EhExecutivo { get; set; } = false;
        public bool EhLegislativo { get; set; } = false;
        public string Nome { get; set; }
        public bool VotosValidos { get; set; } = false;
        public int Idade { get; set; }
        public int QtdVotos { get; set; }
        public string TipoCandidatura { get; set; }
        public string FotoURL { get; set; }
        public int CidadeId { get; set; }
        public CidadeDto Cidade { get; set; }
        public int PartidoId { get; set; }
        public PartidoDto Partido { get; set; }
        public int ColigacaoId { get; set; }
        public ColigacaoDto Coligacao { get; set; }
    }
}
