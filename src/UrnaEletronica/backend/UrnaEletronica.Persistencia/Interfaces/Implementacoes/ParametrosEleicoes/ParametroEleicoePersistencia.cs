﻿

using Microsoft.EntityFrameworkCore;
using UrnaEletronica.Dominio.Modelos.Cidades;
using UrnaEletronica.Dominio.Modelos.ParametrosEleicoes;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.ParametrosEleicoes;
using UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.ConfigEleicoes
{
    public class ParametroEleicoePersistencia : SharedPersistencia, IParametroEleicaoPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public ParametroEleicoePersistencia(UrnaEletronicaContexto contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public async Task<IEnumerable<ParametroEleicao>> GetParametrosAsync()
        {
            IQueryable<ParametroEleicao> query = _contexto.ParametrosEleicoes
                .AsNoTracking()
                .Include(pe => pe.Cidade)
                .OrderBy(u => u.Id);

            return await query.ToListAsync();
        }

        public async Task<ParametroEleicao> GetParametroByIdAsync(int parametroId)
        {
            IQueryable<ParametroEleicao> query = _contexto.ParametrosEleicoes
              .AsNoTracking()
              .OrderBy(u => u.Id)
              .Where(u => u.Id == parametroId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
