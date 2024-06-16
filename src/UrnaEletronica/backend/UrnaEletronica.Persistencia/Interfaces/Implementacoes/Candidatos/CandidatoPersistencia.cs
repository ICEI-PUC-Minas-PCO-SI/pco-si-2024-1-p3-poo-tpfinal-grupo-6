using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Dominio.Modelos.Candidatos;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Candidatos;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Candidatos
{
    public class CandidatoPersistencia : SharedPersistencia, ICandidatoPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public CandidatoPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Candidato>> GetAllCandidatosAsync()
        {
            IQueryable<Candidato> query = _contexto.Candidatos
                .Include(c => c.Cidade)
                .AsNoTracking()
                .OrderBy(a => a.Id);

            return await query.ToListAsync();
        }

        public async Task<Candidato> GetCandidatoByIdAsync(int candidatoId)
        {
            IQueryable<Candidato> query = _contexto.Candidatos
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Where(c => c.Id == candidatoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> RegistrarVoto(int candidatoId)
        {
            var candidato = _contexto.Candidatos
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == candidatoId);

            candidato.QtdVotos += 1;

            Update(candidato);

            return await SaveChangeAsync();
        }

        public async Task<bool> RegistrarVoto(int candidatoId, int qtdVotos)
        {
            var candidato = _contexto.Candidatos
                .AsNoTracking()
                .FirstOrDefault(a => a.Id == candidatoId);

            candidato.QtdVotos += qtdVotos;

            Update(candidato);

            return await SaveChangeAsync();
        }
    }
}
