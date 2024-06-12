using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Candidatos
{
    public interface ICandidatoPersistencia : ISharedPersistencia
    {
        Task<IEnumerable<Candidato>> GetAllCandidatosAsync();
        Task<Candidato> GetCandidatoByIdAsync(int candidatoId);
        Task<bool> RegistrarVoto(int candidatoId);
        Task<bool> RegistrarVoto(int candidatoId, int qtdVotos);
    }
}
