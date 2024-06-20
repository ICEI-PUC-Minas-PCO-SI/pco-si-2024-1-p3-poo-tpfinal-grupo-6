using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Servico.Dtos.Candidatos;
using UrnaEletronica.Servico.Dtos.Cidades;

namespace UrnaEletronica.Servico.Servicos.Contratos.Candidatos
{
    public interface ICandidatoServico
    {
        Task<CandidatoDto> CreateCandidato(CandidatoDto candidatoDto);
        Task<CandidatoDto> UpdateCandidato(int candidatoId, CandidatoDto candidatoDto);
        Task<bool> DeleteCandidato(int candidatoId);
        Task<IEnumerable<CandidatoDto>> GetAllCandidatosComVotosValidosExecutivoAsync();
        Task<IEnumerable<CandidatoDto>> GetAllCandidatosComVotosValidosLegislativoAsync();
        Task<IEnumerable<CandidatoDto>> GetAllCandidatosAsync();
        Task<CandidatoDto> GetCandidatoByIdAsync(int candidatoId);
        Task<bool> RegistrarVoto(int candidatoId);
        Task<bool> RegistrarVoto(int candidatoId, int qtdVotos);
    }
}
