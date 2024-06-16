using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Servico.Dtos.Cidades;

namespace UrnaEletronica.Servico.Dtos.Partidos
{
    public class PartidoDto
    {
        public int Id { get; set; }
        public int CodigoPartido { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public IEnumerable<CidadeDto> Cidades { get; set; }
    }
}
