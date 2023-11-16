using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario GetUsuarioByAlias(string userAlias);
        Usuario GetUsuario(string userAlias, string userPassword);
        Usuario GetUsuarioById(int id);
    }
}
