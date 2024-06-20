using AutoMapper;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Eleicoes;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Executivos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Resultados;
using UrnaEletronica.Servico.Dtos.Resultado;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Eleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.ParametrosEleicoes;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Eleicoes
{
    public class EleicaoExecutivaServico : IEleicaoExecutivaServico
    {
        private readonly IMapper _mapper;
        private readonly ICandidatoServico _candidatoServico;
        private readonly IParametroEleicaoServico _parametroEleicaoServico;
        private readonly EleicaoExecutivo _eleicaoExecutivo;
        private readonly IResultadoPersistencia _resultadoPersistencia;

        public EleicaoExecutivaServico(IMapper mapper, ICandidatoServico candidatoServico, IParametroEleicaoServico parametroEleicaoServico, EleicaoExecutivo eleicaoExecutivo, IResultadoPersistencia resultadoPersistencia)
        {
            _mapper = mapper;
            _candidatoServico = candidatoServico;
            _parametroEleicaoServico = parametroEleicaoServico;
            _eleicaoExecutivo = eleicaoExecutivo;
            _resultadoPersistencia = resultadoPersistencia;
        }

        public async Task<IEnumerable<ResultadoDto>> CalcularResultado()
        {
            try
            {
                var candidados = await _candidatoServico.GetAllCandidatosComVotosValidosExecutivoAsync();
                
                var parametroEleicao = await _parametroEleicaoServico.GetParametroEleicaoAsync();

                var candidadtosMapper = _mapper.Map<Candidato[]>(candidados);

                var parametroEleicaoMapper = _mapper.Map<ParametroEleicao>(parametroEleicao);

                var resultados = _eleicaoExecutivo.CalcularResultado(parametroEleicaoMapper, candidadtosMapper);

                foreach (var resultado in resultados)
                {
                    _resultadoPersistencia.Create<Resultado>(resultado);
                }

                if (await _resultadoPersistencia.SaveChangeAsync())
                {
                    var resultadosRetorno = await _resultadoPersistencia.GetAllResultadosAsync();
                    return _mapper.Map<ResultadoDto[]>(resultadosRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool EleicaoEmAndamento()
        {
            try
            {
                return _eleicaoExecutivo.EleicaoEmAdamento();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool EleicaoEncerrada()
        {
            try
            {
                return _eleicaoExecutivo.EleicaoEncerrada();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public bool EncerrarEleicao()
        {
            try
            {
                return _eleicaoExecutivo.EncerrarEleicao();
            }
            catch (Exception ex)
            {

                throw new Exception($"Falha ao iniciar Eleição. Erro: {ex.Message}");
            }
        }

        public bool IniciarEleicao()
        {
            try
            {
                return _eleicaoExecutivo.IniciarEleicao();
            }
            catch (Exception ex)
            {

                throw new Exception($"Falha ao iniciar Eleição. Erro: {ex.Message}");
            }
        }
    }
}
