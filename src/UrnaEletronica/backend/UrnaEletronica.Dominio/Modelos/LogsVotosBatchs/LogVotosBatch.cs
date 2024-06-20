namespace UrnaEletronica.Dominio.Modelos.LogsVotosBatchs
{
    public class LogVotosBatch
    {
        public int Id { get; set; }
        public DateTime DataHoraRecebimento { get; set; }
        public int CandidatoId { get; set; }
        public int CidadeId { get; set; }
        public int PartidoId { get; set; }
        public int ColigacaoId { get; set; }
        public int QtdVotos { get; set; }
    }
}
