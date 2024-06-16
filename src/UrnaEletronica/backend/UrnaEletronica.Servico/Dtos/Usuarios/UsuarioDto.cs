using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Servico.Dtos.Usuarios
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool IsAdmin { get; set; }
        public string Email { get; set; }
        public string FotoUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } 
    }
}
