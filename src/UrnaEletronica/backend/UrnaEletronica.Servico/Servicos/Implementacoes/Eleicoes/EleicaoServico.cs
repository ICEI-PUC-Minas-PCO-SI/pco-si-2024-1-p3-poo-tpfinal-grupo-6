using AutoMapper;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Eleicoes;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Executivos;
using UrnaEletronica.Dominio.Modelos.Eleicoes.Legislativos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Eleicoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Resultados;
using UrnaEletronica.Servico.Dtos.Eleicoes;
using UrnaEletronica.Servico.Dtos.Resultado;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Eleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.ParametrosEleicoes;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Eleicoes
{
    public class EleicaoServico : IEleicaoServico
    {
        private readonly IMapper _mapper;
        private readonly ICandidatoServico _candidatoServico;
        private readonly IParametroEleicaoServico _parametroEleicaoServico;
        private readonly IEleicaoPersistencia _eleicaoPersistencia;
        private readonly IResultadoPersistencia _resultadoPersistencia;

        public EleicaoServico(
            IMapper mapper, 
            ICandidatoServico candidatoServico, 
            IParametroEleicaoServico parametroEleicaoServico,
            IEleicaoPersistencia eleicaoPersistencia,
            IResultadoPersistencia resultadoPersistencia)
        {
            _mapper = mapper;
            _candidatoServico = candidatoServico;
            _parametroEleicaoServico = parametroEleicaoServico;
            _eleicaoPersistencia = eleicaoPersistencia;
            _resultadoPersistencia = resultadoPersistencia;
        }

        public async Task<IEnumerable<ResultadoDto>> CalcularResultado(int eleicaoId)
        {
            try
            {
                Console.WriteLine("---------------------------------------------------------------");
                var candidatos = await _candidatoServico.GetAllCandidatosComVotosValidosExecutivoAsync();
                
                var parametrosEleicao = await _parametroEleicaoServico.GetParametrosAsync();
                var parametroEleicao = parametrosEleicao.FirstOrDefault();
                
                var candidadtosMapper = _mapper.Map<Candidato[]>(candidatos);
                
                var parametroEleicaoMapper = _mapper.Map<ParametroEleicao>(parametroEleicao);
                
                var eleicao = _eleicaoPersistencia.GetEleicaoByIdUpdateAsync(eleicaoId);
                
                var resultados = eleicao.CalcularResultado(parametroEleicaoMapper, candidadtosMapper);
                Console.WriteLine("resultados ok ||||||||||||||||||||||||||||||||||| " + resultados.Count() );
                var resultadosParaPersistencia = new List<Resultado>();
                foreach (var resultado in resultados)
                {
                    var resultadoParaPersistir = new Resultado
                    {
                        CandidatoId = resultado.CandidatoId,
                        QtdVotos = resultado.QtdVotos,
                        PercentualVotos = resultado.PercentualVotos,
                        CandidatoEleito = resultado.CandidatoEleito,
                    };
                    Console.WriteLine("entrei foreach ok ||||||||||||||||||||||||||||||||||| " + resultado.CandidatoId + " " + resultado.QtdVotos);
                   // _resultadoPersistencia.Create<Resultado>(resultadoParaPersistir);
                    Console.WriteLine("create ok |||||||||||||||||||||||||||||||||||");
                    resultadosParaPersistencia.Add(resultadoParaPersistir);
                }
/*                Console.WriteLine("foreach ok |||||||||||||||||||||||||||||||||||");
                if (await _resultadoPersistencia.SaveChangeAsync())
                {
                    var resultadosRetorno = await _resultadoPersistencia.GetAllResultadosAsync();
                    Console.WriteLine("resultadosRetorno ok |||||||||||||||||||||||||||||||||||");
                    return _mapper.Map<ResultadoDto[]>(resultadosRetorno);
                }*/

                return null;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<bool> EleicaoEmAndamento(int eleicaoId)
        {
            try
            {
                var eleicao = _eleicaoPersistencia.GetEleicaoByIdUpdateAsync(eleicaoId);
                return Task.FromResult(eleicao.EleicaoEmAdamento());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public Task<bool> EleicaoEncerrada(int eleicaoId)
        {
            try
            {
                var eleicao = _eleicaoPersistencia.GetEleicaoByIdUpdateAsync(eleicaoId);
                return Task.FromResult(eleicao.EleicaoEncerrada());
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> EncerrarEleicaoAsync(int eleicaoId)
        {
            try
            {
                var eleicao = _eleicaoPersistencia.GetEleicaoByIdUpdateAsync(eleicaoId);

                if (eleicao == null) return false;

                if (eleicao.EncerrarEleicao())
                {
                    _eleicaoPersistencia.Update<Eleicao>(eleicao);
                }
                return await _eleicaoPersistencia.SaveChangeAsync();
            }
            catch (Exception ex)
            {

                throw new Exception($"Falha ao iniciar Eleição. Erro: {ex.Message}");
            }
        }

        public async Task<bool> IniciarEleicaoAsync()
        {
            try
            {
                var eleicaoExecutivo = new EleicaoExecutivo();

                if (eleicaoExecutivo.IniciarEleicao())
                _eleicaoPersistencia.Create<Eleicao>(eleicaoExecutivo);

                var eleicaoLegislativo = new EleicaoLegislativo();
                
                if(eleicaoLegislativo.IniciarEleicao())
                _eleicaoPersistencia.Create<Eleicao>(eleicaoLegislativo);


                if (await _eleicaoPersistencia.SaveChangeAsync())
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<EleicaoDto>> GetAllEleicoesAsync()
        {
            try
            {
                var eleicoes = await _eleicaoPersistencia.GetAllEleicoesAsync();

                if (eleicoes == null) return null;

                var eleicoesMappper = _mapper.Map<EleicaoDto[]>(eleicoes);

                return eleicoesMappper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<EleicaoDto> GetEleicaoByIdAsync(int eleicaoId)
        {
            try
            {
                var eleicao = await _eleicaoPersistencia.GetEleicaoByIdAsync(eleicaoId);

                if (eleicao == null) return null;

                var eleicaoMapper = _mapper.Map<EleicaoDto>(eleicao);

                return eleicaoMapper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
