using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.Coligacoes;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Coligacoes;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Coligacoes
{
    public class ColigacaoPersistencia : SharedPersistencia, IColigacaoPersistencia
    {

        private readonly UrnaEletronicaContexto _contexto;

        public ColigacaoPersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<Coligacao>> GetAllColigacoesAsync()
        {
            IQueryable<Coligacao> query = _contexto.Coligacoes
                .Include(c => c.Partidos)
                .Include(c => c.Candidatos)
                .AsNoTracking()
                .OrderBy(c => c.Id);

            return await query.ToListAsync();
        }

        public async Task<Coligacao> GetColigacaoByIdAsync(int coligacaoId)
        {
            IQueryable<Coligacao> query = _contexto.Coligacoes
                .Include(c => c.Partidos)
                .Include(c => c.Candidatos)
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .Where(u => u.Id == coligacaoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<bool> CalcularVotosColigacao()
        {
            var coligacoes = _contexto.Coligacoes
                .Include(c => c.Partidos)
                .Include(c => c.Candidatos.Where(c => c.VotosValidos))
                .AsNoTracking();

            foreach (var coligacao in coligacoes)
            {
                int totalVotosValidos = coligacao.Candidatos.Sum(c => c.QtdVotos);
                coligacao.QtdVotos = totalVotosValidos;
                Update(coligacao);
            }

            return await SaveChangeAsync();
        }


    }
}
