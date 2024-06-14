using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Servico.Dtos.Usuarios;

namespace UrnaEletronica.Servico.Servicos.Contratos.Usuarios
{
    public interface ITokenServico
    {
        Task<string> CreateToken(UsuarioUpdateDto usuarioUpdateDto);
    }
}
