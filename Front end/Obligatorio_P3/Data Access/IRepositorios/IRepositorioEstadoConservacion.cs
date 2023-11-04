using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Data_Access.IRepositorios
{
    public interface IRepositorioEstadoConservacion : IRepositorio<EstadoConservacion>
    {
        EstadoConservacion GetEstado(int estado);
    }
}
