using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Cidades;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Servicos.Contratos.Cidades;

namespace UrnaEletronica.Servico.Servicos.Implementacoes.Cidades
{
    public class CidadeServico : ICidadeServico
    {
        private readonly ICidadePersistencia _cidadePersistencia;
        private readonly IMapper _mapper;

        public CidadeServico(ICidadePersistencia cidadePersistencia, IMapper mapper)
        {
            _cidadePersistencia = cidadePersistencia;
            _mapper = mapper;
        }
        public async Task<CidadeDto> CreateCidade(CidadeDto cidadeDto)
        {
            try 
            {
                var cidade = _mapper.Map<Cidade>(cidadeDto);
                _cidadePersistencia.Create<Cidade>(cidade);
                if (await _cidadePersistencia.SaveChangeAsync())
                {
                    var cidadeRetorno = await _cidadePersistencia.GetCidadeByIdAsync(cidade.Id);
                    return _mapper.Map<CidadeDto>(cidadeRetorno);
                }
                return null;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteCidade(int cidadeId)
        {
            try { 
                var cidade= await _cidadePersistencia.GetCidadeByIdAsync(cidadeId);
                if(cidade == null)
                {
                    throw new Exception("Cidade para exclusão não foi encontrada!!");
                }
                _cidadePersistencia.Delete<Cidade>(cidade);
                return await _cidadePersistencia.SaveChangeAsync();
            }catch(Exception ex)
            {
                throw new Exception("Cidade para exclusão não foi encontrada!!");
            }
        }

        public async Task<IEnumerable<CidadeDto>> GetAllCidadesAsync()
        {
            try
            {
                var cidades = await _cidadePersistencia.GetAllCidadesAsync();

                if (cidades == null) return null;

                var cidadesMapper = _mapper.Map<CidadeDto[]>(cidades);

                return cidadesMapper;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CidadeDto> GetCidadeByIdAsync(int cidadeId)
        {
                try
                {
                    var cidade = await _cidadePersistencia.GetCidadeByIdAsync(cidadeId);

                    if (cidade == null) return null;

                    var cidadeMapper = _mapper.Map<CidadeDto>(cidade);

                    return cidadeMapper;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }

            public async Task<CidadeDto> UpdateCidade(int cidadeId, CidadeDto cidadeDto)
        {
            try
            {
                var cidade = await _cidadePersistencia.GetCidadeByIdAsync(cidadeId);

                if (cidade == null) return null;

                var cidadeUpdate = _mapper.Map(cidadeDto, cidade);

                _cidadePersistencia.Update<Cidade>(cidadeUpdate);

                if (await _cidadePersistencia.SaveChangeAsync()) 
                {
                    var cidadeMapper = await _cidadePersistencia.GetCidadeByIdAsync(cidadeUpdate.Id);
                    return _mapper.Map<CidadeDto>(cidadeMapper);
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
