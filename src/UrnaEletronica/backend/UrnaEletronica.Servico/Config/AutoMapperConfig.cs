using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.Usuarios;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Cidades;
using UrnaEletronica.Servico.Dtos.Usuarios;

namespace UrnaEletronica.Servico.Config
{
    public class AutoMapperConfig : Profile
    {

        public AutoMapperConfig()
        {
            CreateMap<Candidato, CandidatoDto>().ReverseMap();

            CreateMap<Cidade, CidadeDto>().ReverseMap();

            CreateMap<Usuario, UsuarioDto>().ReverseMap();
            CreateMap<Usuario, UsuarioUpdateDto>().ReverseMap();
            CreateMap<Usuario, UsuarioLoginDto>().ReverseMap();
        }
    }
}
