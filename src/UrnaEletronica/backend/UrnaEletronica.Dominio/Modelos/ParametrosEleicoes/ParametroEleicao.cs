using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;

namespace UrnaEletronica.Dominio.Modelos.ParametrosEleicoes
{
    public class ParametroEleicao
    {
        public  int Id { get; set; }
        public bool PrimeiroTurno { get; set; }
        public bool SegundoTurno { get; set; }
        public int QtdVotosSomentePrimeiroTurno { get; set; }
        public int QtdCadeiras {  get; set; }
        public DateTime DataEleicaoPrimeiroTurno { get; set; }
        public DateTime DataEleicaoSegundoTurno { get; set; }
        public int CidadeId { get; set; }
        public Cidade Cidade { get; set; }
    }
}
