using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPaises.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> Index();
        Task<T> Add(T pais);
        Task<T> Get(int id);
        Task<T> Update(int id, T pais);
        Task<bool> Delete(int id);
    }
}
