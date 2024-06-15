using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Candidatos;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Servicos.Contratos.Candidatos;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Candidatos
{
    public class CandidatoServico : ICandidatoServico
    {
        private readonly ICandidatoPersistencia _candidatoPersistencia;
        private readonly IMapper _mapper;

        public CandidatoServico(ICandidatoPersistencia candidatoPersistencia, IMapper mapper)
        {
            _candidatoPersistencia = candidatoPersistencia;
            _mapper = mapper;
        }
        public async Task<CandidatoDto> CreateCandidato(CandidatoDto candidatoDto)
        {
            try
            {
                var candidato = _mapper.Map<Candidato>(candidatoDto);

                _candidatoPersistencia.Create<Candidato>(candidato);

                if (await _candidatoPersistencia.SaveChangeAsync())
                {
                    var candidatoRetorno = await _candidatoPersistencia.GetCandidatoByIdAsync(candidato.Id);

                    return _mapper.Map<CandidatoDto>(candidatoRetorno);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteCandidato(int candidatoId)
        {
            try
            {
                var candidato = await _candidatoPersistencia.GetCandidatoByIdAsync(candidatoId);

                if (candidato == null)
                    throw new Exception("Candidato para exclusão náo foi encontrado!");

                _candidatoPersistencia.Delete<Candidato>(candidato);

                return await _candidatoPersistencia.SaveChangeAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<CandidatoDto>> GetAllCandidatosAsync()
        {
            try
            {
                var candidatos = await _candidatoPersistencia.GetAllCandidatosAsync();

                if (candidatos == null) return null;

                var candidatosMappper = _mapper.Map<CandidatoDto[]>(candidatos);

                return candidatosMappper;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CandidatoDto> GetCandidatoByIdAsync(int candidatoId)
        {
            try
            {
                var candidato = await _candidatoPersistencia.GetCandidatoByIdAsync(candidatoId);

                if (candidato == null) return null;

                var candidatoMapper = _mapper.Map<CandidatoDto>(candidato);

                return candidatoMapper;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RegistrarVoto(int candidatoId)
        {
            var candidato = await _candidatoPersistencia.GetCandidatoByIdAsync(candidatoId);

            if (candidato == null) return false;

            var votoRegistrado = await _candidatoPersistencia.RegistrarVoto(candidatoId);

            return votoRegistrado;
        }

        public async Task<bool> RegistrarVoto(int candidatoId, int qtdVotos)
        {
            var candidato = await _candidatoPersistencia.GetCandidatoByIdAsync(candidatoId);

            if (candidato == null) return false;

            var votoRegistrado = await _candidatoPersistencia.RegistrarVoto(candidatoId, qtdVotos);

            return votoRegistrado;
        }

        public async Task<CandidatoDto> UpdateCandidato(int candidatoId, CandidatoDto candidatoDto)
        {
            try
            {
                var candidato = await _candidatoPersistencia.GetCandidatoByIdAsync(candidatoId);

                if (candidato == null) return null;

                var candidatoUpdate = _mapper.Map(candidatoDto, candidato);

                _candidatoPersistencia.Update<Candidato>(candidatoUpdate);

                if (await _candidatoPersistencia.SaveChangeAsync())
                {
                    var candidatoMapper = await _candidatoPersistencia.GetCandidatoByIdAsync(candidatoMapper.Id);
                    return _mapper.Map<CandidatoDto>(candidatoMapper);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
