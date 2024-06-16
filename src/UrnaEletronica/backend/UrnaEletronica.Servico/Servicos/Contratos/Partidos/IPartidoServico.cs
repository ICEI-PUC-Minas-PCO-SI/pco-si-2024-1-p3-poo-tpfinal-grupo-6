using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Servico.Dtos.Partidos;

namespace UrnaEletronica.Servico.Servicos.Contratos.Partidos
{
    public interface IPartidoServico
    {
        Task<IEnumerable<PartidoDto>> GetAllPartidosAsync();
        Task<PartidoDto> GetPartidoByIdAsync(int partidoId);
        Task<PartidoDto> CreatePartido(PartidoDto partidoDto);
        Task<PartidoDto> UpdatePartido(int partidoId, PartidoDto partidoDto);
        Task<bool> DeletePartido(int partidoId);
    }
}
