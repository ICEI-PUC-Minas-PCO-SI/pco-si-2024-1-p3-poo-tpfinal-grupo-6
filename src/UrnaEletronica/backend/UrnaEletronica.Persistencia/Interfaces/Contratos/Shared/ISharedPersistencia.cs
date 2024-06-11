using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UrnaEletronica.Persistencia.Interfaces.Contratos.Shared
{
    public interface ISharedPersistencia
    {
       void Create<T>(T entity) where T : class; 
       void Update<T>(T entity) where T : class;
       void Delete<T>(T entity) where T : class;
       void DeleteRange<T>(T[] entity) where T : class;
       Task<bool> SaveChangeAsync();
    }
}
