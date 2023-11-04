using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioUsuario: IRepositorio<Usuario>
    {
        Usuario GetUsuarioByAlias(string alias);
        Usuario GetUsuarioById(int id);
    }
}
