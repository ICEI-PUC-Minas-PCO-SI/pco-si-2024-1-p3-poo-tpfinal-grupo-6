using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Resultados;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Candidatos;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Partidos;
using UrnaEletronica.Servico.Dtos.Resultado;
using UrnaEletronica.Servico.Servicos.Contratos.Resultados;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Resultados
{
    public class ResultadoServico : IResultadoServico
    {
        private readonly IResultadoPersistencia _resultadoPersistencia;
        private readonly IMapper _mapper;

        public ResultadoServico(IResultadoPersistencia resultadoPersistencia, IMapper mapper)
        {
            _resultadoPersistencia = resultadoPersistencia;
            _mapper = mapper;
        }

        public async Task<ResultadoDto> CreateResultado(ResultadoDto resultadoDto)
        {
            try
            {
                var resultado = _mapper.Map<Resultado>(resultadoDto);
                _resultadoPersistencia.Create<Resultado>(resultado);

                if (await _resultadoPersistencia.SaveChangeAsync())
                {
                    var resultadoRetorno = await _resultadoPersistencia.GetResultadoByIdAsync(resultado.Id);

                    return _mapper.Map<ResultadoDto>(resultadoRetorno);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteResultado(int resultadoId)
        {
            try
            {
                var resultado = await _resultadoPersistencia.GetResultadoByIdAsync(resultadoId);

                if (resultado == null)
                    throw new Exception("Resultado para exclusão náo foi encontrado!");

                _resultadoPersistencia.Delete<Resultado>(resultado);

                return await _resultadoPersistencia.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ResultadoDto>> GetAllResultadosAsync()
        {
            try
            {
                var resultados = await _resultadoPersistencia.GetAllResultadosAsync();

                if (resultados == null) return null;

                var resultadosMappper = _mapper.Map<ResultadoDto[]>(resultados);

                return resultadosMappper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultadoDto> GetResultadoByIdAsync(int resultadoId)
        {
            try
            {
                var resultado = await _resultadoPersistencia.GetResultadoByIdAsync(resultadoId);

                if (resultado == null) return null;

                var resultadoMapper = _mapper.Map<ResultadoDto>(resultado);

                return resultadoMapper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ResultadoDto> UpdateResultado(int resultadoId, ResultadoDto resultadoDto)
        {
            try
            {
                var resultado = await _resultadoPersistencia.GetResultadoByIdAsync(resultadoId);

                if (resultado == null) return null;

                var resultadoUpdate = _mapper.Map(resultadoDto, resultado);

                _resultadoPersistencia.Update<Resultado>(resultadoUpdate);

                if (await _resultadoPersistencia.SaveChangeAsync())
                {
                    var resultadoMapper = await _resultadoPersistencia.GetResultadoByIdAsync(resultadoUpdate.Id);
                    return _mapper.Map<ResultadoDto>(resultadoMapper);
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
