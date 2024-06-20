using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Dominio.Modelos.Usuarios
{
    public class Usuario : IdentityUser<int>
    {
        public required string Nome { get; set; }
        public bool IsAdmin { get; set; }
        public string FotoURL { get; set; }
        public IEnumerable<UsuarioFuncao> UsuariosFuncoes { get; set; }
    }
}
