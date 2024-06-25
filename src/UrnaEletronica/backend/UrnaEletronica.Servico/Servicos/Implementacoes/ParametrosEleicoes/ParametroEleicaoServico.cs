using AutoMapper;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.ParametrosEleicoes;
using UrnaEletronica.Servico.Dtos.ParametrosEleicoes;
using UrnaEletronica.Servico.Servicos.Contratos.ParametrosEleicoes;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.ParametrosEleicoes
{
    public class ParametroEleicaoServico : IParametroEleicaoServico
    {
        private readonly IParametroEleicaoPersistencia _parametroEleicaoPersistencia;
        private readonly IMapper _mapper;

        public ParametroEleicaoServico(IParametroEleicaoPersistencia parametroEleicaoPersistencia, IMapper mapper)
        {
            _parametroEleicaoPersistencia = parametroEleicaoPersistencia;
            _mapper = mapper;
        }

        public async Task<ParametroEleicaoDto> CreateParametroEleicao(ParametroEleicaoDto parametroEleicaoDto)
        {
            try
            {
                var parametroEleicao = _mapper.Map<ParametroEleicao>(parametroEleicaoDto);

                _parametroEleicaoPersistencia.Create<ParametroEleicao>(parametroEleicao);

                if (await _parametroEleicaoPersistencia.SaveChangeAsync())
                {
                    var parametroEleicaoRetorno = await _parametroEleicaoPersistencia.GetParametroByIdAsync(parametroEleicao.Id);

                    return _mapper.Map<ParametroEleicaoDto>(parametroEleicaoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<IEnumerable<ParametroEleicaoDto>> GetParametrosAsync()
        {
            try
            {
                var parametroEleicao = await _parametroEleicaoPersistencia.GetParametrosAsync();

                if (parametroEleicao == null) return null;

                var parametrpEleicaoMappper = _mapper.Map<ParametroEleicaoDto[]>(parametroEleicao);

                return parametrpEleicaoMappper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ParametroEleicaoDto> GetParametroByIdAsync(int parametroId)
        {
            try
            {
                var parametroEleicao = await _parametroEleicaoPersistencia.GetParametroByIdAsync(parametroId);

                if (parametroEleicao == null) return null;

                var parametrpEleicaoMappper = _mapper.Map<ParametroEleicaoDto>(parametroEleicao);

                return parametrpEleicaoMappper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ParametroEleicaoDto> UpdateParametroEleicao(int parametroEleicaoId, ParametroEleicaoDto parametroEleicaoDto)
        {
            try
            {
                var parametroEleicao = _mapper.Map<Candidato>(parametroEleicaoDto);

                _parametroEleicaoPersistencia.Create<Candidato>(parametroEleicao);

                if (await _parametroEleicaoPersistencia.SaveChangeAsync())
                {
                    var parametroEleicaoRetorno = await _parametroEleicaoPersistencia.GetParametroByIdAsync(parametroEleicaoId);

                    return _mapper.Map<ParametroEleicaoDto>(parametroEleicaoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
