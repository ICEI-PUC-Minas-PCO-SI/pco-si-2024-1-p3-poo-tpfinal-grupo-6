using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Servico.Servicos.Contratos.Log
{
    public interface IProcessarVotosBatchServico
    {
        Task ProcessarArquivos();
    }
}
