using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;

namespace UrnaEletronica.Dominio.Modelos.Partidos
{
    public class Partido
    {
        public int Id { get; set; }
        public int CodigoPartido { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public IEnumerable<Cidade> Cidades { get; set; }
    }
}
