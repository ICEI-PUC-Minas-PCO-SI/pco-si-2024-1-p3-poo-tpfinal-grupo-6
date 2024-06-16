using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Partidos;
using UrnaEletronica.Servico.Dtos.Cidades;

namespace UrnaEletronica.Servico.Dtos.Candidatos
{
    public class CandidatoDto
    {
        public int Id { get; set; }
        public int ExecutivoId { get; set; }
        public int LegislativoId { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int QtdVotos { get; set; }
        public int CidadeId { get; set; }
        public CidadeDto Cidade { get; set; }
        public int PartidoId { get; set; }
        //public PartidoDto Partido { get; set; }
        public int ColigacaoId { get; set; }
        //public ColigacaoDto Coligacao { get; set; 
    }
}
