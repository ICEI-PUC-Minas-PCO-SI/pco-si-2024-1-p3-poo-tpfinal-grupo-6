using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;

namespace UrnaEletronica.Servico.Dtos.Eleicoes
{
    public class EleicaoDto 
    {
        public int Id { get; set; }
        public bool IniciarVotacao { get; set; } = false;
        public bool EncerrarVotacao { get; set; } = false;
        public DateTime DataHoraInicioVotacao { get; set; }
        public DateTime DataHoraFimVotacao { get; set; }

    }
}
