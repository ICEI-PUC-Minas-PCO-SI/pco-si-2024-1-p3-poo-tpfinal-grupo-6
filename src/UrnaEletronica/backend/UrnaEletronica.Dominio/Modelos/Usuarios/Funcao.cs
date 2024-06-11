using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Dominio.Modelos.Usuarios
{
    public class Funcao : IdentityRole<int>
    {
        public string NomeFuncao { get; set; }
        public IEnumerable<UsuarioFuncao> UsuariosFuncoes { get; set; }
    }
}
