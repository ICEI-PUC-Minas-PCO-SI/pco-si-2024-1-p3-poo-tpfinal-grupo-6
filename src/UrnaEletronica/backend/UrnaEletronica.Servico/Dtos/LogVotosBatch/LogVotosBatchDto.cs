using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Servico.Dtos.LogVotosBatch
{
    public class LogVotosBatchDto
    {
        public int Id { get; set; }
        public DateTime DataHoraRecebimento { get; set; }
        public int CandidatoId { get; set; }
        public int CidadeId { get; set; }
        public int PartidoId { get; set; }
        public int ColigacaoId { get; set; }
        public int qtdVotos { get; set; }
    }
}
