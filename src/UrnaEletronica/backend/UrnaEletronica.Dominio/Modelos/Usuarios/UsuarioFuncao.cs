using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Dominio.Modelos.Usuarios
{
    public class UsuarioFuncao : IdentityUserRole<int>
    {
        public Usuario Usuario { get; set; }
        public Funcao Funcao { get; set; }
    }
}
