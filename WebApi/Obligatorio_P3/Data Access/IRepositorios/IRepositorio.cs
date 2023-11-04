using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorio<T> {
        T Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        IEnumerable<T> GetAll();
        void Save();

    }
}
