using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IRestContext<T>
    {
        Task<IEnumerable<T>> GetAll(String filters);
        Task<T> GetById(int id);
        Task<T> Add(T entity, string token);
        Task<bool> Update(int id, T entity);
        Task<bool> Remove(int id);
    }
}
