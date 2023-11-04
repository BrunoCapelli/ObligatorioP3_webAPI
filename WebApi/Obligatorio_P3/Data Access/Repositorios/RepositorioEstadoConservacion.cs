using Data_Access.IRepositorios;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Repositorios
{
    public class RepositorioEstadoConservacion: Repositorio<EstadoConservacion>, IRepositorioEstadoConservacion
    {
        public RepositorioEstadoConservacion(MiContexto context) { 
            Context = context;
        }

        public IEnumerable<EstadoConservacion> GetAll()
        {
            return Context.EstadosConservacion;
        }

        public EstadoConservacion GetEstado(int estado)
        {
            EstadoConservacion Estado = Context.EstadosConservacion.FirstOrDefault(ec => ec.EstadoConservacionId == estado);
            return Estado;
        }
    }
}
