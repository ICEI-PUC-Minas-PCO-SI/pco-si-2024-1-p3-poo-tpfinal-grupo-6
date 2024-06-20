
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Coligacoes;

namespace UrnaEletronica.Servico.Dtos.Partidos
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int ColigacaoId { get; set; }
        public ColigacaoDto Coligacao { get; set; }
        public IEnumerable<CandidatoDto> Candidatos { get; set; }
    }
}
