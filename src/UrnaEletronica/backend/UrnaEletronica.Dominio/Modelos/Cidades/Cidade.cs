using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Dominio.Modelos.Cidades
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int QtdHabitantes { get; set; }

        
    }
}
