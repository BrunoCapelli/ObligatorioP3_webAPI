using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataAccess
{
    public interface IRestContextUsuario<T>
    {
        Task<Usuario> Login(string alias, string password);
        Task<T> Add(T entity, string token);
        Task<IEnumerable<T>> GetAll(string filters);
    }
}
