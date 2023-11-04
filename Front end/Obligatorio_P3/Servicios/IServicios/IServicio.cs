using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IServicio<T> where T : class
    {
        T Add(T entity);
        void Update(T entity);
        void Remove(int id);
    }
}