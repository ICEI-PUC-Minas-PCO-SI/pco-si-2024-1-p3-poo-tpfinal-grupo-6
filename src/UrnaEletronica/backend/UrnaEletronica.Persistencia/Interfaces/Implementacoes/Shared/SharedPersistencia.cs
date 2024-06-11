using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrnaEletronica.Persistencia.Contexto;
using UrnaEletronica.Persistencia.Interfaces.Contratos.Shared;

namespace UrnaEletronica.Persistencia.Interfaces.Implementacoes.Shared
{
    public class SharedPersistencia : ISharedPersistencia
    {
        private readonly UrnaEletronicaContexto _contexto;

        public SharedPersistencia(UrnaEletronicaContexto contexto)
        {
            _contexto = contexto;
        }
        public void Create<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _contexto.RemoveRange(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return ((await _contexto.SaveChangesAsync())>0);
        }

        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }
    }
}
