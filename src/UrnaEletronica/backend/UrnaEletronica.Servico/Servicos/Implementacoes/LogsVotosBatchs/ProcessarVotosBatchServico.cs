using UrnaEletronica.Dominio.Modelos.LogsVotosBatchs;
using UrnaEletronica.Persistencia.Interfaces.Contratos.LogsVotosBatchs;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Log;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.LogsVotosBatchs
{
    public class ProcessarVotosBatchServico : IProcessarVotosBatchServico
    {
        private readonly ILogVotosBatchPersistencia _logVotosBatchPersistencia;
        private readonly ILogVotosErrosPersistencia _logVotosErrosPersistencia;
        private readonly ICandidatoServico _candidatoServico;

        public ProcessarVotosBatchServico(ILogVotosBatchPersistencia logVotosBatchPersistencia, ILogVotosErrosPersistencia logVotosErrosPersistencia, ICandidatoServico candidatoServico)
        {
            _logVotosBatchPersistencia = logVotosBatchPersistencia;
            _logVotosErrosPersistencia = logVotosErrosPersistencia;
            _candidatoServico = candidatoServico;
        }
        public async Task ProcessarArquivos()
        {
			try
			{
				string diretorio = "C:/votosBatch";

				foreach (string arquivo in Directory.GetFiles(diretorio))
				{
					var linahs = await File.ReadAllLinesAsync(arquivo);

					foreach (var linha in linahs)
					{
						var campos = linha.Split(',');

						var id = int.Parse(campos[0]);
						DateTime dataHoraRecebimento = DateTime.Now;
						var candidatoId = int.Parse(campos[1]);
						var cidadeId = int.Parse(campos[2]);
						var partidoId = int.Parse(campos[3]);
						var coligacaoId = int.Parse(campos[4]);
						var qtdVotos = int.Parse(campos[5]);

						var candidato = await _candidatoServico.GetCandidatoByIdAsync(candidatoId);

						bool registroSalvo;

						if (candidato == null)
						{
							var logVotosBatchErros = new LogVotosBatchErros
							{
								DataHoraRecebimento = DateTime.Now,
								CandidatoId = candidatoId,
								CidadeId = cidadeId,
								PartidoId = partidoId,
								ColigacaoId = coligacaoId,
								QtdVotos = qtdVotos,
								MensagemErro = "Candidato não encontrado"
							};

							_logVotosErrosPersistencia.Create(logVotosBatchErros);

							registroSalvo = await _logVotosErrosPersistencia.SaveChangeAsync();
						}
						else if (candidato.CidadeId != cidadeId)
						{
							var logVotosBatchErros = new LogVotosBatchErros
							{
								DataHoraRecebimento = DateTime.Now,
								CandidatoId = candidatoId,
								CidadeId = cidadeId,
								PartidoId = partidoId,
								ColigacaoId = coligacaoId,
								QtdVotos = qtdVotos,
								MensagemErro = "Candidato não pertence à cidade informada."
							};

							_logVotosErrosPersistencia.Create(logVotosBatchErros);

                            registroSalvo = await _logVotosErrosPersistencia.SaveChangeAsync();
						}
						else if (candidato.PartidoId != partidoId)
						{
							var logVotosBatchErros = new LogVotosBatchErros
							{
								DataHoraRecebimento = DateTime.Now,
								CandidatoId = candidatoId,
								CidadeId = cidadeId,
								PartidoId = partidoId,
								ColigacaoId = coligacaoId,
								QtdVotos = qtdVotos,
								MensagemErro = "Candidato não pertence ao partido informado."
							};

							_logVotosErrosPersistencia.Create(logVotosBatchErros);

                            registroSalvo = await _logVotosErrosPersistencia.SaveChangeAsync();
						}
						else if (candidato.ColigacaoId != coligacaoId)
						{
                            var logVotosBatchErros = new LogVotosBatchErros
                            {
                                DataHoraRecebimento = DateTime.Now,
                                CandidatoId = candidatoId,
                                CidadeId = cidadeId,
                                PartidoId = partidoId,
                                ColigacaoId = coligacaoId,
                                QtdVotos = qtdVotos,
                                MensagemErro = "Candidato não pertence à coligacao informada."
                            };

                            _logVotosErrosPersistencia.Create(logVotosBatchErros);

                            registroSalvo = await _logVotosErrosPersistencia.SaveChangeAsync();
                        } else
						{
                            var logVotosBatch = new LogVotosBatch
                            {
                                DataHoraRecebimento = DateTime.Now,
                                CandidatoId = candidatoId,
                                CidadeId = cidadeId,
                                PartidoId = partidoId,
                                ColigacaoId = coligacaoId,
                                QtdVotos = qtdVotos
                            };

                            _logVotosBatchPersistencia.Create(logVotosBatch);

                            registroSalvo = await _logVotosBatchPersistencia.SaveChangeAsync();
                        }

						if (!registroSalvo)
						{
							throw new Exception("Falha inesperada ao salvar os votos dos candidados.}");
						}
					}
				}
			}
			catch (Exception ex)
			{
                throw new Exception(ex.Message);
            }
        }
    }
}
