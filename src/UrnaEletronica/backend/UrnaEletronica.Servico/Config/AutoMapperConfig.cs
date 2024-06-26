﻿using AutoMapper;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Dominio.Modelos.Eleicoes;
using UrnaEletronica.Dominio.Modelos.LogsVotosBatchs;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Dominio.Modelos.Partidos;
using UrnaEletronica.Dominio.Modelos.Resultados;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Dtos.Coligacoes;
using UrnaEletronica.Servico.Dtos.Eleicoes;
using UrnaEletronica.Servico.Dtos.LogVotosBatch;
using UrnaEletronica.Servico.Dtos.ParametrosEleicoes;
using UrnaEletronica.Servico.Dtos.Partidos;
using UrnaEletronica.Servico.Dtos.Resultado;
using UrnaEletronica.Servico.Dtos.Usuarios;

namespace UrnaEletronica.Servico.Config
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<Candidato, CandidatoDto>().ReverseMap();

            CreateMap<Cidade, CidadeDto>().ReverseMap();

            CreateMap<Coligacao, ColigacaoDto>().ReverseMap();

            CreateMap<Eleicao, EleicaoDto>().ReverseMap();
            
            CreateMap<LogVotosBatch, LogVotosBatchDto>().ReverseMap();
            CreateMap<LogVotosBatchErros, LogVotosBatchErrosDto>().ReverseMap();

            CreateMap<ParametroEleicao, ParametroEleicaoDto>().ReverseMap();

            CreateMap<Partido, PartidoDto>().ReverseMap();

            CreateMap<Resultado, ResultadoDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();

        }
    }
}
