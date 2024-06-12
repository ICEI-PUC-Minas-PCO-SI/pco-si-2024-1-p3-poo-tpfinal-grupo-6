using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;

namespace UrnaEletronica.Dominio.Modelos.Candidatos
{
    public class Candidato
    {
        public int Id { get; set; }
        public int ExecutivoId { get; set; }
        public int LegislativoId { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int QtdVotos { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
        public int PartidoId { get; set; }
        //public Partido Partido { get; set; }
    }
}
