using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Partidos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Partidos;
using UrnaEletronica.Servico.Dtos.Partidos;
using UrnaEletronica.Servico.Servicos.Contratos.Partidos;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Partidos
{
    public class PartidoServico : IPartidoServico
    {
        private readonly IPartidoPersistencia _partidoPersistencia;
        private readonly IMapper _mapper;

        public PartidoServico(IPartidoPersistencia partidoPersistencia, IMapper mapper)
        {
            _partidoPersistencia = partidoPersistencia;
            _mapper = mapper;
        }
        public async Task<PartidoDto> CreatePartido(PartidoDto partidoDto)
        {
            try
            {
                var partido = _mapper.Map<Partido>(partidoDto);
                _partidoPersistencia.Create<Partido>(partido);
                if (await _partidoPersistencia.SaveChangeAsync())
                {
                    var partidoRetorno = await _partidoPersistencia.GetPartidoByIdAsync(partido.Id);
                    return _mapper.Map<PartidoDto>(partidoRetorno);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeletePartido(int partidoId)
        {
            try
            {
                var partido = await _partidoPersistencia.GetPartidoByIdAsync(partidoId);
                if (partido == null)
                {
                    throw new Exception("Partido para exclusão não foi encontrado!!");
                }
                _partidoPersistencia.Delete<Partido>(partido);
                return await _partidoPersistencia.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Partido para exclusão não foi encontrado!!");
            }
        }
        public async Task<IEnumerable<PartidoDto>> GetAllPartidosAsync()
        {
            try
            {
                var partidos = await _partidoPersistencia.GetAllPartidosAsync();

                if (partidos == null) return null;

                var partidosMapper = _mapper.Map<PartidoDto[]>(partidos);

                return partidosMapper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PartidoDto> GetPartidoByIdAsync(int partidoId)
        {
            try
            {
                var partido = await _partidoPersistencia.GetPartidoByIdAsync(partidoId);

                if (partido == null) return null;

                var partidoMapper = _mapper.Map<PartidoDto>(partido);

                return partidoMapper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public async Task<PartidoDto> UpdatePartido(int partidoId, PartidoDto partidoDto)
        {
            try
            {
                var partido = await _partidoPersistencia.GetPartidoByIdAsync(partidoId);

                if (partido == null) return null;

                var partidoUpdate = _mapper.Map(partidoDto, partido);

                _partidoPersistencia.Update<Partido>(partidoUpdate);

                if (await _partidoPersistencia.SaveChangeAsync())
                {
                    var partidoMapper = await _partidoPersistencia.GetPartidoByIdAsync(partidoUpdate.Id);
                    return _mapper.Map<PartidoDto>(partidoMapper);
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
