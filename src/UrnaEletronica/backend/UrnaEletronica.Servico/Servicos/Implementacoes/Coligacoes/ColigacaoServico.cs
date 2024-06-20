using AutoMapper;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Coligacoes;
using UrnaEletronica.Servico.Dtos.Coligacoes;
using UrnaEletronica.Servico.Servicos.Contratos.Coligacoes;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Coligacoes
{
    public class ColigacaoServico : IColigacaoServico
    {
        private readonly IColigacaoPersistencia _coligacaoPersistencia;
        private readonly IMapper _mapper;

        public ColigacaoServico(IColigacaoPersistencia coligacaoPersistencia, IMapper mapper)
        {
            _coligacaoPersistencia = coligacaoPersistencia;
            _mapper = mapper;
        }

        public async Task<bool> CalcularVotosColigacao()
        {
            try
            {
                return await _coligacaoPersistencia.CalcularVotosColigacao();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ColigacaoDto> CreateColigacao(ColigacaoDto coligacaoDto)
        {
            try
            {
                var coligacao = _mapper.Map<Coligacao>(coligacaoDto);
                _coligacaoPersistencia.Create<Coligacao>(coligacao);
                if (await _coligacaoPersistencia.SaveChangeAsync())
                {
                    var coligacaoRetorno = await _coligacaoPersistencia.GetColigacaoByIdAsync(coligacao.Id);
                    return _mapper.Map<ColigacaoDto>(coligacaoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteColigacao(int coligacaoId)
        {
            try
            {
                var coligacao = await _coligacaoPersistencia.GetColigacaoByIdAsync(coligacaoId);

                if (coligacao == null)
                    throw new Exception("Coligacao para exclusão não foi encontrado!");

                _coligacaoPersistencia.Delete<Coligacao>(coligacao);

                return await _coligacaoPersistencia.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ColigacaoDto>> GetAllColigacoesAsync()
        {
            try
            {
                var coligacoes = await _coligacaoPersistencia.GetAllColigacoesAsync();

                if (coligacoes == null) return null;

                var coligacoesMappper = _mapper.Map<ColigacaoDto[]>(coligacoes);

                return coligacoesMappper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ColigacaoDto> GetColigacaoByIdAsync(int coligacaoId)
        {
            try
            {
                var coligacao = await _coligacaoPersistencia.GetColigacaoByIdAsync(coligacaoId);

                if (coligacao == null) return null;

                var coligacaoMapper = _mapper.Map<ColigacaoDto>(coligacao);

                return coligacaoMapper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ColigacaoDto> UpdateColigacao(int coligacaoId, ColigacaoDto coligacaoDto)
        {
            try
            {
                var coligacao = await _coligacaoPersistencia.GetColigacaoByIdAsync(coligacaoId);

                if (coligacao == null) return null;

                var coligacaoUpdate = _mapper.Map(coligacaoDto, coligacao);

                _coligacaoPersistencia.Update<Coligacao>(coligacaoUpdate);

                if (await _coligacaoPersistencia.SaveChangeAsync())
                {
                    var coligacaoMapper = await _coligacaoPersistencia.GetColigacaoByIdAsync(coligacaoUpdate.Id);
                    return _mapper.Map<ColigacaoDto>(coligacaoMapper);
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
