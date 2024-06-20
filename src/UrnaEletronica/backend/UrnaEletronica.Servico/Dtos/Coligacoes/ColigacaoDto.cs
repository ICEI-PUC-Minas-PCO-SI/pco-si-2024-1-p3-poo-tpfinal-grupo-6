using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Partidos;

namespace UrnaEletronica.Servico.Dtos.Coligacoes
{
    public class ColigacaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int QtdVotos { get; set; }
        public IEnumerable<PartidoDto> Partido { get; set; }
        public IEnumerable<CandidatoDto> Candidatos { get; set; }

    }
}
