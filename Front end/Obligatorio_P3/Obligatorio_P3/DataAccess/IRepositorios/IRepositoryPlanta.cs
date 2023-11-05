using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess.IRepositorios
{
    public interface IRepositoryPlanta<T> : IRepository<T>
    {
        IEnumerable<T> GetByName(string name);
        T GetById(int id);
        IEnumerable<T> GetBetweenDates(DateTime from, DateTime to);

    }
}
