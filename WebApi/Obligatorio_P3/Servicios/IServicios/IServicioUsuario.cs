using Domain.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.IServicios
{
    public interface IServicioUsuario: IServicio<UsuarioDTO>
    {
        UsuarioDTO FindAlias(UsuarioDTO user);
        UsuarioDTO FindUser(UsuarioDTO user);
    }
}
