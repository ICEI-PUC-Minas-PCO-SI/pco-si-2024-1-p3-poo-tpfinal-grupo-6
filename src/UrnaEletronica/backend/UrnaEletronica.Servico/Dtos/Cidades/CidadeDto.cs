﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Servico.Dtos.Cidades
{
    public class CidadeDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SiglaEstado { get; set; }
        public string NomeEstado { get; set; }
        public int QtdHabitantes { get; set; }

    }
}
